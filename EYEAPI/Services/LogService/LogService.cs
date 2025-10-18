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

        public Task<List<LogDto>> GetLogsAsync(LogSearchParamsDto searchParams)
        {
            eyeRepository.
        }

        public async Task<LogDto> AddLogAsync(CreateLogDto newLog) =>  mapper.Map<LogDto>(await eyeRepository.AddLogAsync(mapper.Map<Log>(newLog)));
        

        public Task<LogDto> UpdateLogAsync(Log log)
        {
            throw new NotImplementedException();
        }
        public Task DeleteLogByIdAsync(int locationId)
        {
            throw new NotImplementedException();
        }
    }
}
