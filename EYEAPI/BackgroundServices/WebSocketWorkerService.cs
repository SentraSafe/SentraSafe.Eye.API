using EYEAPI.Models;
using EYEAPI.Services.MqttService;
using MQTTnet.Protocol;
using MQTTnet;
using MongoDB.Driver;
using System.Text.Json;
using EYEAPI.Models.Entities;
using EYEAPI.Hubs;
using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace EYEAPI.BackgroundServices
{
    public class WebSocketWorkerService(
        IHubContext<MachineHub> machineHubContext,
        IHubContext<AlarmHub> alarmHubContext,
        MqttClientOptionsBuilder mqttClientOptions,
        IMqttService mqttService,
        IOptions<AppSettings> appSettings,
        IServiceScopeFactory scopeFactory) : BackgroundService
    {
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            var brokerSettings = appSettings.Value.MqttBroker;
            var x = mqttClientOptions.WithCredentials(brokerSettings.Users[1], brokerSettings.Secrets[1]);

            await mqttService.ConnectAsync(mqttClientOptions.Build());
            await mqttService.SubscribeAsync("measurement/#", MqttQualityOfServiceLevel.AtLeastOnce, OnMessageReceived);
            await machineHubContext.Clients.All.SendAsync("Hub is Online!");
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

            if (telemetry == null) return;

            using IServiceScope serviceScope = scopeFactory.CreateScope();

            IEyeRepository eyeRepository = serviceScope.ServiceProvider.GetRequiredService<IEyeRepository>();

            List<Alarm> alarms = await eyeRepository.GetAlarmsAsync(new AlarmSearchParamsDto() { MachineId = telemetry.MachineId, Value = (int)telemetry.Value });

            List<EventLog> eventLogs = alarms.Select(x => new EventLog()
            {
                MachineId = x.MachineId,
                AlarmId = x.Id,
                Severity = x.Severity,
                Source = x.Title,
                Message = telemetry.Value.ToString(),
                TimeCreated = DateTime.Now
            }).ToList();
            await eyeRepository.AddEventLogsAsync(eventLogs);

            await alarmHubContext.Clients.Groups($"{AlarmHub.AlarmsGroupPrefix}{telemetry.MachineId.ToString()}").SendAsync("update", eventLogs);
            await machineHubContext.Clients.Groups($"{MachineHub.MachineGroupPrefix}{telemetry.MachineId.ToString()}").SendAsync("update", telemetry);
            return;
        }
    }
}