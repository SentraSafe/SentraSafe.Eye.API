using EYEAPI.Services;
using EYEAPI.Repositories;
using EYEAPI.Models;
using System.Diagnostics.Metrics;
using System.Text.Json;
using MQTTnet;
using System.Text;
using System.Diagnostics;

namespace EYEAPI.BackgroundServices
{
    public class SensorWorkerService(ILogger<SensorWorkerService> logger, IMqttService mqttService, IServiceScopeFactory scopeFactory) : BackgroundService
    {
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await mqttService.Connect();
            await mqttService.Subscribe("measurement/DHT11-1/temperature", OnMessageReceived);
            Console.WriteLine("Background service started");
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
            Console.WriteLine($"-----------------\n{telemetry.Id}\n{telemetry.Device}\n{telemetry.MeasurementType}\n{telemetry.Location}\n{telemetry.ReadingTime}\n{telemetry.Value}");
            return;

            if (telemetry != null)
            {
                AsyncServiceScope scope = scopeFactory.CreateAsyncScope();
                IMeasurementRepository measurementRepository = scope.ServiceProvider.GetRequiredService<IMeasurementRepository>();

                //await measurementRepository.AddMeasurement(telemetry);
            }
        }
    }
}
