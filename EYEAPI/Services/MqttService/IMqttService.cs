using MQTTnet.Protocol;
using MQTTnet;

namespace EYEAPI.Services.MqttService
{
    public interface IMqttService
    {
        bool IsConnected { get; }

        Task ConnectAsync(MqttClientOptions options);
        Task PublishAsync(string topic, dynamic payload, MqttQualityOfServiceLevel qos, bool retain);
        Task PublishJsonAsync(string topic, dynamic payload, MqttQualityOfServiceLevel qos, bool retain);
        Task ReconnectAsync();
        Task SubscribeAsync(string topic, MqttQualityOfServiceLevel qos, Func<MqttApplicationMessageReceivedEventArgs, Task> callback);
        Task UnsubscribeAsync(string topic);
    }
}
