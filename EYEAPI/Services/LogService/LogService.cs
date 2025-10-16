using AutoMapper;
using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Repositories;
using MongoDB.Driver.Search;

namespace EYEAPI.Services.LogService
{
    public class LogService(IEyeRepository eyeRepository, IMapper mapper) : ILogService
    {
        public async Task<List<Alarm>> GetAlarmsAsync(AlarmSearchParamsDto searchParams) => await eyeRepository.GetAlarmsAsync(searchParams);
        public async Task<Alarm> AddAlarmAsync(CreateAlarmDto createAlarm) => await eyeRepository.AddAlarmAsync(mapper.Map<Alarm>(createAlarm));

        public async Task<Alarm> UpdateAlarmAsync(Alarm updateAlarm)
        {
            Alarm editAlarm = mapper.Map<Alarm>(updateAlarm);
            return mapper.Map<Alarm>(await eyeRepository.UpdateAlarmAsync(editAlarm));
        }
        public async Task DeleteAlarmByIdAsync(int alarmId) => await eyeRepository.DeleteAlarmByIdAsync(alarmId);
    }
}
