using AutoMapper;
using EYEAPI.Contexts;
using EYEAPI.Models.Dtos.LogDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;
using EYEAPI.Services.LogService;
using Humanizer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EYEAPI.Hubs;

public class AlarmHub(ILogService logService, EyeContext eyeContext, IMapper mapper) : Hub
{
    #region alarmSubscrition

    public static string AlarmGroupPrefix = "machine_";
    public static string AlarmsGroupPrefix = "machines_";

    public async Task SubscribeToAlarm(string group)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"{AlarmGroupPrefix}{group}");
    }

    public async Task UnsubscribeToAlarm(string group)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{AlarmGroupPrefix}{group}");
    }

    #endregion

    public async Task<List<EventLogDto>> SubscribeToAlarms(string[] groups)
    {
        foreach (string group in groups)
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{AlarmsGroupPrefix}{group}");

        int[] ids = groups.Select(x =>
        {
            int.TryParse(x, out int machineId);
            return machineId;
        }).Where(x => x != 0).ToArray();

        return (await logService.GetEventLogsAsync(new EventLogSearchParamsDto()
        {
            MachineIds = ids,
            Severity = Severity.Warning,
            AlarmIdNotNull = true
        })).DistinctBy(x => x.MachineId).ToList();
    }

    public async Task<List<EventLogDto>> SubscribeToNotifications()
    {
        var notificationList = await eyeContext.EventLogs
            .Where(x => x.AlarmId != null)
            .Include(x => x.Alarm)
            .Include(x => x.Machine)
            .GroupBy(x => x.Alarm.ValueType)
            .Select(group => new 
            {
                ValueType = group.Key,
                EventLog = group.OrderByDescending(x => x.Severity).FirstOrDefault()
            })
            .ToListAsync();

        return mapper.Map<List<EventLogDto>>( mapper.Map<List<EventLogDto>>(notificationList.Select(x => new EventLogDto()
        {
            Severity = x.EventLog.Severity,
            IsHandled = x.EventLog.IsHandled,
            MachineId = x.EventLog.MachineId,
            AlarmId = x.EventLog.AlarmId,
            HandledAt = x.EventLog.HandledAt,
            HandledBy = x.EventLog.HandledBy,
            HandledFeedback = x.EventLog.HandledFeedback,
            Id = x.EventLog.Id,
            Message = x.EventLog.Message,
            Source = x.EventLog.Source,
            TimeCreated = x.EventLog.TimeCreated
        }).ToList()));
    }

    public async Task UnsubscribeToAlarms(string[] groups)
    {
        foreach (string group in groups)
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{AlarmsGroupPrefix}{group}");
    }
}