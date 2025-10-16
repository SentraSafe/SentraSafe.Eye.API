using EYEAPI.Repositories;
using System.Diagnostics.Metrics;
using System.Text.Json;
using MQTTnet;
using System.Text;
using System.Diagnostics;
using MongoDB.Driver;
using EYEAPI.Models.Entities;
using EYEAPI.Services.MqttService;
using EYEAPI.Models;
using MQTTnet.Protocol;

namespace EYEAPI.BackgroundServices
{
    public class SensorWorkerService(MqttClientOptionsBuilder mqttClientOptions,ILogger<SensorWorkerService> logger, IMqttService mqttService, IServiceScopeFactory scopeFactory, MongoClient mongoClient,AppSettings appSettings) : BackgroundService
    {
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            var x = mqttClientOptions.WithCredentials(appSettings.MqttBroker.Users[0], appSettings.MqttBroker.Secrets[0]);
            
            await mqttService.ConnectAsync(mqttClientOptions.Build());
            await mqttService.SubscribeAsync("measurement/#",MqttQualityOfServiceLevel.AtLeastOnce, OnMessageReceived);
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
            await mongoClient.GetDatabase("SensorData").GetCollection<Measurement>("Sensor").InsertOneAsync(telemetry);
            
            return;
        }
    }
}
