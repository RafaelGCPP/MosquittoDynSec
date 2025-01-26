namespace DynSec.MQTT
{
    public class MQTTConfig
    {
        public string? Host { get; set; }
        public int Port { get; set; } = 8883;
        public bool Tls { get; set; } = true;
        public bool WebSockets { get; set; } = false;
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Endpoint { get; set; } = "mqtt";
        public string? ClientId { get; set; } = "DynSec.API";
        public string Uri
        {
            get
            {
                if (!WebSockets)
                {
                    return $"{Host}:{Port}";
                }
                var protocol = Tls ? "wss://" : "ws://";
                return $"{protocol}://{Host}:{Port}/{Endpoint}";
            }
        }


    }
}
