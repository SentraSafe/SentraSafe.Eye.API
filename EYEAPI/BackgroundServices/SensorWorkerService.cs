using EYEAPI.Repositories;
using System.Diagnostics.Metrics;
using System.Text.Json;
using MQTTnet;
using System.Text;
using System.Diagnostics;
using MongoDB.Driver;
using EYEAPI.Models.Entities;
using EYEAPI.Services.MqttService;

namespace EYEAPI.BackgroundServices
{
    public class SensorWorkerService(ILogger<SensorWorkerService> logger, IMqttService mqttService, IServiceScopeFactory scopeFactory, MongoClient mongoClient) : BackgroundService
    {
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await mqttService.Connect();
            await mqttService.Subscribe("measurement/#", OnMessageReceived);
#if DEBUG
            Console.WriteLine("Background service started");
#endif
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!mqttService.ClientIsConnected)
            {
                await mqttService.Reconnect();
            }        
        }
                                             
        private async Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            var payload = eventArgs.ApplicationMessage.ConvertPayloadToString();
            Measurement? telemetry = JsonSerializer.Deserialize<Measurement>(payload, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
#if DEBUG
            Console.WriteLine($"-----------------\n{telemetry.ReadingTime}\n{telemetry.MeasurementType}\n{telemetry.Device}\n{telemetry.Value}\n{telemetry.Location}\n{DateTimeOffset.FromUnixTimeSeconds(telemetry.ReadingTime).ToUniversalTime()}");
#endif
            await mongoClient.GetDatabase("SensorData").GetCollection<Measurement>("Sensor").InsertOneAsync(telemetry);
            
            return;
        }
    }
}
