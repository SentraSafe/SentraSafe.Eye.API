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
using Microsoft.Extensions.Options;

namespace EYEAPI.BackgroundServices
{
    public class SensorWorkerService(
        MqttClientOptionsBuilder mqttClientOptions,
        ILogger<SensorWorkerService> logger,
        IMqttService mqttService,
        IServiceScopeFactory scopeFactory,
        MongoClient mongoClient,
        IOptions<AppSettings> appSettings) : BackgroundService
    {
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting Sensor Worker Service");
            var brokerSettings = appSettings.Value.MqttBroker;
            var x = mqttClientOptions.WithCredentials(brokerSettings.Users[0], brokerSettings.Secrets[0]);

            await mqttService.ConnectAsync(x.Build());
            await mqttService.SubscribeAsync("measurement/#", MqttQualityOfServiceLevel.AtLeastOnce, OnMessageReceived);
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Sensor Worker Service is running.");
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!mqttService.IsConnected)
                {
                    await mqttService.ReconnectAsync();
                }

                await Task.Delay(5000, stoppingToken);
            }
        }

        private async Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            var payload = eventArgs.ApplicationMessage.ConvertPayloadToString();
            Measurement? telemetry = JsonSerializer.Deserialize<Measurement>(payload, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Console.WriteLine(telemetry.Device);
            Console.WriteLine(telemetry.MachineId);
            Console.WriteLine(telemetry.MeasurementType);
            Console.WriteLine(telemetry.ReadingTime);
            Console.WriteLine(telemetry.Location);
            await mongoClient.GetDatabase("Eye").GetCollection<Measurement>("Telemetry").InsertOneAsync(telemetry);

            return;
        }
    }
}