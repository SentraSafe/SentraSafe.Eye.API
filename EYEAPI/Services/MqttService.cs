using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Protocol;
using System.Text.Json;
using System.Text;
using EYEAPI.Models;

namespace EYEAPI.Services
{
    public class MqttService(MqttClientFactory mqttClientFactory, IMqttClient mqttClient, MqttClientOptionsBuilder builder, IOptions<AppSettings> options) : IMqttService
    {
        private readonly IOptions<AppSettings> _options = options;

        public bool ClientIsConnected => mqttClient.IsConnected;

        public async Task<MqttClientConnectResult> Connect(MqttClientOptionsBuilder? optionsBuilder = null, CancellationToken? cancellationToken = null)
        {
            optionsBuilder ??= builder;
            return await mqttClient.ConnectAsync(optionsBuilder.Build(), cancellationToken ?? CancellationToken.None);
        }

        public async Task Reconnect(CancellationToken? cancellationToken = null)
        {
            await mqttClient.ReconnectAsync(cancellationToken ?? CancellationToken.None);
        }


        public async Task Subscribe(string topic, Func<MqttApplicationMessageReceivedEventArgs, Task> onMessageReceivedEvent, MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtLeastOnce, CancellationToken? cancellationToken = null)
        {
            MqttClientSubscribeOptions mqttClientSubscribeOptions = mqttClientFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(topic, qos)
                .Build();

            mqttClient.ApplicationMessageReceivedAsync += onMessageReceivedEvent;

            if (!ClientIsConnected)
            {
                await Reconnect();
            }

            await mqttClient.SubscribeAsync(mqttClientSubscribeOptions, cancellationToken ?? CancellationToken.None);
        }

        public MqttClientOptionsBuilder GetOptionsBuilder()
        {
            return builder;
        }

        public IMqttClient GetClient()
        {
            return mqttClient;
        }
    }
}
