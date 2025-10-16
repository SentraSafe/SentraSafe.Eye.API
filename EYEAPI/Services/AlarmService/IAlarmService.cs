using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Entities;

namespace EYEAPI.Services.AlarmService
{
    public interface IAlarmService
    {
        Task<List<Alarm>> GetAlarmsAsync(AlarmSearchParamsDto searchParams);
    }
}
