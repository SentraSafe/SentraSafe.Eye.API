using EYEAPI.Services.MqttService;
using MQTTnet.Protocol;
using MQTTnet;
using System.Text;
using System.Text.Json;

public class MqttService(MqttClientFactory factory) : IMqttService
{
    private readonly IMqttClient _client = factory.CreateMqttClient();


    public async Task ConnectAsync(MqttClientOptions options)
    {
        if (!_client.IsConnected)
        {
            await _client.ConnectAsync(options);
        }
    }

    public async Task ReconnectAsync()
    {
        await _client.ReconnectAsync();
    }

    public async Task SubscribeAsync(string topic, MqttQualityOfServiceLevel qos, Func<MqttApplicationMessageReceivedEventArgs, Task> callback)
    {
        MqttClientSubscribeOptionsBuilder subscribeOptionsBuilder = factory.CreateSubscribeOptionsBuilder();
        MqttClientSubscribeOptions subscribeOptions = subscribeOptionsBuilder.WithTopicFilter(topic, qos).Build();
        _client.ApplicationMessageReceivedAsync += callback;
        await _client.SubscribeAsync(subscribeOptions);
    }

    public async Task UnsubscribeAsync(string topic)
    {
        MqttClientUnsubscribeOptionsBuilder unsubscribeOptionsBuilder = factory.CreateUnsubscribeOptionsBuilder();
        MqttClientUnsubscribeOptions unsubscribeOptions = unsubscribeOptionsBuilder.WithTopicFilter(topic).Build();
        await _client.UnsubscribeAsync(unsubscribeOptions);
    }

    public async Task PublishJsonAsync(string topic, dynamic payload, MqttQualityOfServiceLevel qos, bool retain)
    {
        byte[] serializedMessage = JsonSerializer.SerializeToUtf8Bytes(payload);
        MqttApplicationMessage mqttMessage = factory.CreateApplicationMessageBuilder()
                                                    .WithTopic(topic)
                                                    .WithContentType("application/json")
                                                    .WithPayload(serializedMessage)
                                                    .WithQualityOfServiceLevel(qos)
                                                    .WithRetainFlag(retain)
                                                    .Build();
        await _client.PublishAsync(mqttMessage);
    }

    public async Task PublishAsync(string topic, dynamic payload, MqttQualityOfServiceLevel qos, bool retain)
    {
        byte[] serializedMessage = Encoding.Default.GetBytes(payload.ToString());
        MqttApplicationMessage mqttMessage = factory.CreateApplicationMessageBuilder()
            .WithTopic(topic)
            .WithContentType("text/plain")
            .WithPayload(serializedMessage)
            .WithQualityOfServiceLevel(qos)
            .WithRetainFlag(retain)
            .Build();
        await _client.PublishAsync(mqttMessage);
    }

    public bool IsConnected => _client.IsConnected;
}