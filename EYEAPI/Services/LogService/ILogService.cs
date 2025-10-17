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
        Task<List<LogDto>> GetLogsAsync(LogSearchParamsDto searchParams);

        Task<LogDto> PostNewLogAsync(CreateLogDto newLog);
        Task<LogDto> UpdateAsync(LogDto log);
        Task DeleteSublocationByIdAsync(int locationId);
    }
}
