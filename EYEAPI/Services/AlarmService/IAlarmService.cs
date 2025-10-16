using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Entities;

namespace EYEAPI.Services.AlarmService
{
    public interface IAlarmService
    {
        Task<Alarm> AddAlarmAsync(CreateAlarmDto createAlarm);
        Task DeleteAlarmByIdAsync(int alarmId);
        Task<List<Alarm>> GetAlarmsAsync(AlarmSearchParamsDto searchParams);
        Task<Alarm> UpdateAlarmAsync(Alarm updateAlarm);
    }
}
