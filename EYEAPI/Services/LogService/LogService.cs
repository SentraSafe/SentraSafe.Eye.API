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

        public async Task<List<LogDto>> GetLogsAsync(LogSearchParamsDto searchParams)
        {
            return mapper.Map<List<LogDto>>(await eyeRepository.GetLogsAsync(searchParams));
        }

        public async Task<LogDto> AddLogAsync(CreateLogDto newLog)
        {
            Log mappedNewLog = mapper.Map<Log>(newLog);
            await eyeRepository.AddLogAsync(mappedNewLog);
            return mapper.Map<LogDto>(await eyeRepository.GetLogByIdAsync(mappedNewLog.Id));
        }
        

        public async Task<LogDto> UpdateLogAsync(Log log)
        {
            await eyeRepository.UpdateLogAsync(log);
            return mapper.Map<LogDto>(await eyeRepository.GetLogByIdAsync(log.Id));
        }
        public async Task DeleteLogByIdAsync(int logId)
        {
            await eyeRepository.DeleteLogByIdAsync(logId);
        }
    }
}
