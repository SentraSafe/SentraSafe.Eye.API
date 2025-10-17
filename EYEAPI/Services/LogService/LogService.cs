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
        public Task DeleteSublocationByIdAsync(int locationId)
        {
            throw new NotImplementedException();
        }

        public Task<List<LogDto>> GetLogsAsync(LogSearchParamsDto searchParams)
        {
            throw new NotImplementedException();
        }

        public Task<LogDto> PostNewLogAsync(CreateLogDto newLog)
        {
            throw new NotImplementedException();
        }

        public Task<LogDto> UpdateAsync(LogDto log)
        {
            throw new NotImplementedException();
        }
    }
}
