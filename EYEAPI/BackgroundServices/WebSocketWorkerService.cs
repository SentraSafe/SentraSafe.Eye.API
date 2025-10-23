using EYEAPI.Models;
using EYEAPI.Services.MqttService;
using MQTTnet.Protocol;
using MQTTnet;
using MongoDB.Driver;
using System.Text.Json;
using EYEAPI.Contexts;
using EYEAPI.Models.Entities;
using EYEAPI.Hubs;
using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Enums;
using EYEAPI.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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
            Console.WriteLine("Starting WebSocket Worker Service");
            var brokerSettings = appSettings.Value.MqttBroker;
            var x = mqttClientOptions.WithCredentials(brokerSettings.Users[1], brokerSettings.Secrets[1]).WithClientId(nameof(WebSocketWorkerService)).WithCleanSession(false);

            await mqttService.ConnectAsync(x.Build());
            await mqttService.SubscribeAsync("measurement/#", MqttQualityOfServiceLevel.AtLeastOnce, OnMessageReceived);
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("WebSocket Worker Service is running.");
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
            Console.WriteLine("Message received");
            var payload = eventArgs.ApplicationMessage.ConvertPayloadToString();
            Measurement? telemetry = JsonSerializer.Deserialize<Measurement>(payload, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (telemetry == null) return;
            using IServiceScope serviceScope = scopeFactory.CreateScope();

            EyeContext eyeContext = serviceScope.ServiceProvider.GetRequiredService<EyeContext>();

            Machine? machine = await eyeContext.Machines.Include(machine => machine.Alarms).Select(x => new Machine()
            {
                Id = x.Id,
                Name = x.Name,
                Sublocation = x.Sublocation,
                Type = x.Type,
                SublocationId = x.SublocationId,
                LatestLog = x.EventLogs != null ? x.EventLogs.OrderByDescending(y => y.TimeCreated).First(y => y.Alarm != null && y.Alarm.ValueType == telemetry.MeasurementType) : null,
                Alarms = x.Alarms != null ? x.Alarms.Where(y => y.ValueType == telemetry.MeasurementType).ToList() : null
            }).FirstOrDefaultAsync(m => m.Id == telemetry.MachineId);

            List<Alarm>? alarms = machine?.Alarms?.Where(x => (machine.LatestLog == null || machine.LatestLog.IsHandled) || (machine.LatestLog.Severity == Severity.Warning && x.Severity > Severity.Warning)).ToList();

            List<EventLog> eventLogs = alarms?.Select(x => new EventLog()
            {
                MachineId = x.MachineId,
                AlarmId = x.Id,
                Severity = x.Severity,
                Source = x.Title,
                Message = telemetry.Value.ToString(),
                TimeCreated = DateTime.Now
            }).ToList();

            if (eventLogs != null)
            {
                eyeContext.EventLogs.AddRange(eventLogs);
                await eyeContext.SaveChangesAsync();
                await alarmHubContext.Clients.All.SendAsync("notifications", eventLogs);
                await alarmHubContext.Clients.Groups($"{AlarmHub.AlarmsGroupPrefix}{telemetry.MachineId.ToString()}").SendAsync("updateEvents", eventLogs);
            }

            await machineHubContext.Clients.Groups($"{MachineHub.MachineGroupPrefix}{telemetry.MachineId.ToString()}").SendAsync("update", telemetry);
        }
    }
}