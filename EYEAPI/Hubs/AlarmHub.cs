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

    public async Task<List<EventLogDto>> SubscribeToAlarms(string[] groups)
    {
        foreach (string group in groups)
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{AlarmsGroupPrefix}{group}");

        int[] ids = groups.Select(x =>
        {
            int.TryParse(x, out int machineId);
            return machineId;
        }).Where(x => x != 0).ToArray();

        return (await logService.GetEventLogsAsync(new EventLogSearchParamsDto() { MachineIds = ids, Severity = Severity.Warning, AlarmIdNotNull = true })).DistinctBy(x => x.MachineId).ToList();
    }
    
    public async Task<List<EventLogDto>> SubscribeToNotifications()
    {
        List<EventLog?> eventLogs = await eyeContext.Machines.Select(x => x.EventLogs != null ? x.EventLogs.OrderByDescending(y => y.TimeCreated).First(y => y.Alarm != null) : null).ToListAsync();

        return mapper.Map<List<EventLogDto>>( eventLogs.Where(x => x != null && !x.IsHandled && x.Severity > Severity.Information).ToList());
    }

    public async Task UnsubscribeToAlarms(string[] groups)
    {
        foreach (string group in groups)
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{AlarmsGroupPrefix}{group}");
    }
}