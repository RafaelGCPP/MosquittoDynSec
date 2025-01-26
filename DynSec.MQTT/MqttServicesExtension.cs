using Microsoft.Extensions.DependencyInjection;
using MQTTnet;

namespace DynSec.MQTT
{
    public static class MqttServicesExtension
    {
        public static void AddMqttOptions(this IServiceCollection services, MQTTConfig mqttConfig)
        {
            MqttClientOptionsBuilder mqttClientOptionsBuilder = new();
            if (mqttConfig.WebSockets)
            {
                mqttClientOptionsBuilder = mqttClientOptionsBuilder.WithWebSocketServer(o => o.WithUri(mqttConfig.Uri))
                    .WithCredentials(mqttConfig.UserName, mqttConfig.Password);
            }
            else
            {
                mqttClientOptionsBuilder = mqttClientOptionsBuilder.WithTcpServer(mqttConfig.Host, mqttConfig.Port)
                    .WithCredentials(mqttConfig.UserName, mqttConfig.Password); ;
            }

            if (mqttConfig.Tls)
                mqttClientOptionsBuilder = mqttClientOptionsBuilder.WithTlsOptions(new MqttClientTlsOptionsBuilder().Build());

            var mqttClientOptions = mqttClientOptionsBuilder
                .WithClientId(mqttConfig.ClientId)
                .WithCleanSession(true)
                .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V500)
                .WithNoKeepAlive()
                .Build();

            services.AddSingleton(mqttClientOptions);
        }

        public static void AddMqttClient(this IServiceCollection services)
        {
            services.AddSingleton<IMqttClient>(sp =>
           {
               MqttClientFactory mqttFactory = new();
               var client = mqttFactory.CreateMqttClient();
               return client;
           });
        }
    }
}


