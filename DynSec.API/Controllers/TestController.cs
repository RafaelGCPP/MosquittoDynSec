using Microsoft.AspNetCore.Mvc;
using MQTTnet;

namespace DynSec.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> logger;

        public TestController(ILogger<TestController> logger)
        {
            this.logger = logger;
        }

        [HttpGet(Name = "GetTest")]
        public string Get(IMqttClient client, MqttClientOptions mqttClientOptions)
        {
            client.ConnectAsync(mqttClientOptions).Wait();
            if (client.IsConnected)
            {
                return "Connected";
            }
            else
            {
                return "Not connected";
            }
        }
    }
}
