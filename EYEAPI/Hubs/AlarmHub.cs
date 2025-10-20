using EYEAPI.Models.Dtos.LogDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;
using EYEAPI.Services.LogService;
using Humanizer;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EYEAPI.Hubs;

public class AlarmHub(ILogService logService) : Hub
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

    public async Task<List<LogDto>> SubscribeToAlarms(string[] groups)
    {
        foreach (string group in groups)
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{AlarmsGroupPrefix}{group}");

        return await logService.GetLogsAsync(new LogSearchParamsDto() { IsHandled = false, Severity = Severity.Warning });
    }

    public async Task UnsubscribeToAlarms(string[] groups)
    {
        foreach (string group in groups)
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{AlarmsGroupPrefix}{group}");
    }
}