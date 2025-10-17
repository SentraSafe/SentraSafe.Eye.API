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

        public async Task<LogDto> AddLogAsync(CreateLogDto newLog) =>  mapper.Map<LogDto>(await eyeRepository.AddLogAsync(mapper.Map<Log>(newLog)));
        

        public async Task<LogDto> UpdateLogAsync(Log log)
        {
            return mapper.Map<LogDto>(await eyeRepository.UpdateLogAsync(log));
        }
        public async Task DeleteLogByIdAsync(int locationId)
        {
            await eyeRepository.DeleteLocationByIdAsync(locationId);
        }
    }
}
