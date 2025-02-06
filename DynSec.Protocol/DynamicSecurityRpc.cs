using DynSec.Model.Commands.Abstract;
using DynSec.Model.Commands.TopLevel;
using DynSec.Model.Responses.Abstract;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol.Interfaces;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Extensions.Rpc;
using MQTTnet.Protocol;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace DynSec.Protocol
{
    public class DynamicSecurityRpc: IDynamicSecurityRpc
    {
        private readonly IMqttClient client;
        private readonly MqttClientOptions options;
        private readonly ILogger<DynamicSecurityRpc> logger;
        private readonly MqttRpcClientOptions mqttRpcClientOptions;
        private object transmitting = new object();

        public DynamicSecurityRpc(IMqttClient mqttClient, MqttClientOptions? mqttOptions, ILogger<DynamicSecurityRpc> _logger)
        {
            client = mqttClient;
            options = mqttOptions ?? new();
            logger = _logger;
            mqttRpcClientOptions = new MqttRpcClientOptions()
            {
                TopicGenerationStrategy = new DynSecTopicStrategy()
            };
            
        }

        public async Task<ResponseList> ExecuteAsync(TimeSpan timeout, CommandsList commands)
        {
            var mqttFactory = new MqttClientFactory();

            await client.ConnectAsync(options);

            using (var rpcClient = mqttFactory.CreateMqttRpcClient(client, mqttRpcClientOptions))
            {
                var data = await rpcClient.ExecuteAsync(timeout, "", commands.AsJSON(), MqttQualityOfServiceLevel.AtMostOnce);

                var jsonoptions = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin),
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNameCaseInsensitive = true,
                };
                ResponseList response = JsonSerializer.Deserialize<ResponseList>(data, jsonoptions) ?? new();

                return response;
            }
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


    }

    public sealed class DynSecTopicStrategy : IMqttRpcClientTopicGenerationStrategy
    {
        private const string commandTopic = "$CONTROL/dynamic-security/v1";
        private const string responseTopic = "$CONTROL/dynamic-security/v1/response";

        public MqttRpcTopicPair CreateRpcTopics(TopicGenerationContext context)
        {
            return new MqttRpcTopicPair
            {
                RequestTopic = commandTopic,
                ResponseTopic = responseTopic
            };
        }
    }
}
