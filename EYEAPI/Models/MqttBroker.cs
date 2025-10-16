namespace EYEAPI.Models
{
    public class MqttBroker
    {
        public string Host { get; set; }
        public List<string> Users { get; set; }
        public List<string> Secrets { get; set; }
    }
}
