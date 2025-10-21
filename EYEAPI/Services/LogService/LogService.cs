using AutoMapper;
using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Dtos.LogDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Repositories;
using MongoDB.Driver.Search;

namespace EYEAPI.Services.LogService
{
    public class LogService(IEyeRepository eyeRepository, IMapper mapper) : ILogService
    {
        public async Task<EventLogDto> AddEventLogAsync(CreateEventLogDto newEventLog)
        {
            EventLog eventLog = mapper.Map<EventLog>(newEventLog);
            await eyeRepository.AddEventLogAsync(eventLog);
            return mapper.Map<EventLogDto>(eventLog);
        }

        public async Task HandleEventLog(HandleEventLogDto handleLogDto)
        {
            EventLog? eventLog = await eyeRepository.GetEventLogByIdAsync(handleLogDto.Id);
            if (eventLog is null) return;
            eventLog.HandledBy = handleLogDto.HandledBy;
            eventLog.HandledAt = DateTime.Now;
            eventLog.IsHandled = true;
            eventLog.HandledFeedback = handleLogDto.HandledFeedback;
            await eyeRepository.UpdateEventLogAsync(eventLog);
        }
        
        public async Task<List<EventLogDto>> GetEventLogsAsync(EventLogSearchParamsDto? searchParams) =>        
            mapper.Map<List<EventLogDto>>(await eyeRepository.GetEventLogsAsync(searchParams));
        

        public async Task AddEventLogsAsync(List<CreateEventLogDto> eventLogs) =>    
            await eyeRepository.AddEventLogsAsync(mapper.Map<List<EventLog>>(eventLogs));
    }
}
