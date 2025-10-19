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
        Task<List<LogDto>> GetLogsAsync(LogSearchParamsDto? searchParams);

        Task<LogDto> AddLogAsync(CreateLogDto newLog);
        Task<LogDto> UpdateLogAsync(Log log);
        Task DeleteLogByIdAsync(int locationId);
        Task HandleLog(HandleLogDto handleLogDto);
    }
}
