using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Dtos.LogDtos;
using EYEAPI.Models.Dtos.SublocationDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Services.LogService;
using Microsoft.AspNetCore.Mvc;

namespace EYEAPI.Services.LogService
{
    public interface ILogService
    {
        Task<EventLogDto> AddEventLogAsync(CreateEventLogDto newEventLog);
        Task HandleEventLog(HandleEventLogDto handleEventLogDto);
        Task<List<EventLogDto>> GetEventLogsAsync(EventLogSearchParamsDto searchParams);
        Task AddEventLogsAsync(List<EventLogDto> eventLogs);
    }
}
