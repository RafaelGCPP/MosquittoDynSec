using DynSec.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Extensions.Rpc;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynSec.Protocol
{
    public class DynamicSecurityRpc
    {
        private readonly IMqttClient client;
        private readonly MqttClientOptions options;
        private readonly ILogger<DynamicSecurityHandler> logger;

        public DynamicSecurityRpc(IMqttClient mqttClient, MqttClientOptions? mqttOptions, ILogger<DynamicSecurityHandler> _logger)
        {
            client = mqttClient;
            options = mqttOptions ?? new();
            logger = _logger;

        }
    }
}
