
using EYEAPI.Models;
using EYEAPI.Services.MqttService;
using MQTTnet.Protocol;
using MQTTnet;
using MongoDB.Driver;
using System.Text.Json;
using EYEAPI.Models.Entities;
using EYEAPI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace EYEAPI.BackgroundServices
{
    public class WebSocketWorkerService(IHubContext<MqttHub> hubContext, MqttClientOptionsBuilder mqttClientOptions, ILogger<SensorWorkerService> logger, IMqttService mqttService, IServiceScopeFactory scopeFactory, MongoClient mongoClient, AppSettings appSettings) : BackgroundService
    {
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            var x = mqttClientOptions.WithCredentials(appSettings.MqttBroker.Users[1], appSettings.MqttBroker.Secrets[1]);

            await mqttService.ConnectAsync(mqttClientOptions.Build());
            await mqttService.SubscribeAsync("measurement/#", MqttQualityOfServiceLevel.AtLeastOnce, OnMessageReceived);
            await hubContext.Clients.All.SendAsync("Hub is Online!");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!mqttService.IsConnected)
            {
                await mqttService.ReconnectAsync();
            }
        }

        private async Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            var payload = eventArgs.ApplicationMessage.ConvertPayloadToString();
            Measurement? telemetry = JsonSerializer.Deserialize<Measurement>(payload, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            await hubContext.Clients.Groups(telemetry.MachineId.ToString()).SendAsync("OnMessageRecived", telemetry);
            return;
        }
    }
}
