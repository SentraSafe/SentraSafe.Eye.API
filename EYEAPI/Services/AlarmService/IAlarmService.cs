using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Entities;

namespace EYEAPI.Services.AlarmService
{
    public interface IAlarmService
    {
        Task<AlarmDto> AddAlarmAsync(CreateAlarmDto createAlarm);
        Task DeleteAlarmByIdAsync(int alarmId);
        Task<List<AlarmDto>> GetAlarmsAsync(AlarmSearchParamsDto searchParams);
        Task<AlarmDto> UpdateAlarmAsync(CreateAlarmDto updateAlarm);
    }
}
