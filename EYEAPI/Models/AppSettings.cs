namespace EYEAPI.Models
{
    public class AppSettings
    {
        public MqttBroker MqttBroker { get; set; }

        public ConnectionStrings ConnectionStrings { get; set; }
        public AutoMapperSettings AutoMapper { get; set; }
    }
}
