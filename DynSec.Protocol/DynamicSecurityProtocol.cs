using DynSec.Model.Commands.Abstract;
using DynSec.Model.Commands.TopLevel;
using DynSec.Model.Responses.Abstract;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol.Interfaces;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Exceptions;
using MQTTnet.Internal;
using MQTTnet.Protocol;
using System.Collections.Concurrent;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Timers;
using Timer = System.Timers.Timer;

namespace DynSec.Protocol
{

    public class DynamicSecurityProtocol : IDisposable, IDynamicSecurityProtocol
    {

        readonly IMqttClient client;
        readonly MqttClientOptions options;
        readonly ILogger<DynamicSecurityProtocol> logger;
        Timer watchDog;
        private bool disposedValue;
        private object transmitting = new object();
        private const string commandTopic = "$CONTROL/dynamic-security/v1";
        private const string responseTopic = "$CONTROL/dynamic-security/v1/response";
        readonly ConcurrentDictionary<string, AsyncTaskCompletionSource<ResponseList>> _waitingCalls = new();

        public DynamicSecurityProtocol(IMqttClient mqttClient, MqttClientOptions? mqttOptions, ILogger<DynamicSecurityProtocol> _logger)
        {
            client = mqttClient;
            options = mqttOptions ?? new();
            logger = _logger;
            client.ApplicationMessageReceivedAsync += HandleApplicationMessageReceivedAsync;
            watchDog = new Timer(TimeSpan.FromSeconds(20));
            watchDog.Elapsed += WatchDog_Elapsed;
        }

        private void WatchDog_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (!client.IsConnected) return;
            logger.LogInformation("Watchdog elapsed, disconnecting");
            client.DisconnectAsync().Wait();
            watchDog.Stop();
        }

        public Task<AbstractResponse> ExecuteCommand(AbstractCommand cmd)
        {
            TimeSpan _timeout = TimeSpan.FromSeconds(10);

            var cmds = new CommandsList(new List<AbstractCommand>
            {
                cmd
            });

            ResponseList? responseList;

            // Here we force synchronization, as we don't want to send another command
            // before we receive the response for the previous one. Also, we want to
            // avoid disconnection by session takeover from the next command.

            lock (transmitting)
            {
                var responseListTask = ExecuteAsync(_timeout, cmds);
                responseListTask.Wait();
                responseList = responseListTask.Result;
            }
            var response = responseList.Responses?.First() ?? new GeneralResponse
            {
                Command = cmd.Command,
                Error = "No response received"
            };

            return Task.FromResult(response);
        }
        public async Task<ResponseList> ExecuteAsync(TimeSpan timeout, CommandsList commands)
        {
            using (var timeoutToken = new CancellationTokenSource(timeout))
            {
                try
                {
                    return await ExecuteAsync(commands, timeoutToken.Token).ConfigureAwait(false);
                }
                catch (OperationCanceledException exception)
                {
                    if (timeoutToken.IsCancellationRequested)
                    {
                        throw new MqttCommunicationTimedOutException(exception);
                    }

                    throw;
                }
            }
        }


        public async Task<ResponseList> ExecuteAsync(CommandsList commands, CancellationToken cancellationToken = default)
        {
            try
            {
                var awaitable = new AsyncTaskCompletionSource<ResponseList>();

                if (!_waitingCalls.TryAdd(responseTopic, awaitable))
                {
                    throw new InvalidOperationException();
                }

                var message = new MqttApplicationMessageBuilder()
                    .WithTopic(commandTopic)
                    .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.ExactlyOnce)
                    .WithPayload(commands.AsJSON())
                    .Build();

                var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter(responseTopic)
                    .Build();

                watchDog.Stop();
                watchDog.Start();
                logger.LogInformation("Watchdog reset.");
                if (!client.IsConnected)
                {
                    logger.LogInformation("Client was not connected. Connecting.");
                    await client.ConnectAsync(options, cancellationToken).ConfigureAwait(false);
                    logger.LogInformation("Connected.");

                }

                await client.SubscribeAsync(subscribeOptions, cancellationToken).ConfigureAwait(false);
                await client.PublishAsync(message, cancellationToken).ConfigureAwait(false);
                using (cancellationToken.Register(
                    () =>
                    {
                        awaitable.TrySetCanceled();
                    }))
                {
                    return await awaitable.Task.ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error executing command");
                throw;
            }
            finally
            {
                _waitingCalls.TryRemove(responseTopic, out _);
                await client.UnsubscribeAsync(responseTopic, CancellationToken.None).ConfigureAwait(false);
            }

        }

        Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            if (!_waitingCalls.TryRemove(eventArgs.ApplicationMessage.Topic, out var awaitable))
            {
                return CompletedTask.Instance;
            }

            var message = eventArgs.ApplicationMessage;

            //var payloadBuffer = eventArgs.ApplicationMessage.PayloadSegment.ToArray();
            var payloadStr = message.ConvertPayloadToString();  // Encoding.UTF8.GetString(payloadBuffer);

            var jsonoptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin),
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true,
            };

            var data = JsonSerializer.Deserialize<ResponseList>(payloadStr, jsonoptions) ?? new();



            awaitable.TrySetResult(data);

            // Set this message to handled to that other code can avoid execution etc.
            eventArgs.IsHandled = true;

            return CompletedTask.Instance;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    client.ApplicationMessageReceivedAsync -= HandleApplicationMessageReceivedAsync;
                }

                disposedValue = true;
            }
        }


        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

