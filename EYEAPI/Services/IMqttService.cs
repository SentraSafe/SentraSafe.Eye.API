using MQTTnet.Protocol;
using MQTTnet;

namespace EYEAPI.Services
{
    public interface IMqttService
    {
        Task<MqttClientConnectResult> Connect(MqttClientOptionsBuilder? optionsBuilder = null, CancellationToken? cancellationToken = null);
        Task Reconnect(CancellationToken? cancellationToken = null);
        Task Subscribe(string topic, Func<MqttApplicationMessageReceivedEventArgs, Task> onMessageReceivedEvent, MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtLeastOnce, CancellationToken? cancellationToken = null);
        MqttClientOptionsBuilder GetOptionsBuilder();
        IMqttClient GetClient();
        bool ClientIsConnected { get; }
    }
}
