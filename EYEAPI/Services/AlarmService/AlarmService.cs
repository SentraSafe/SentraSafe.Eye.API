using AutoMapper;
using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Repositories;
using MongoDB.Driver.Search;

namespace EYEAPI.Services.AlarmService
{
    public class AlarmService(IEyeRepository eyeRepository, IMapper mapper) : IAlarmService
    {
        public async Task<List<Alarm>> GetAlarmsAsync(AlarmSearchParamsDto searchParams) => await eyeRepository.GetAlarmsAsync(searchParams);
        public async Task<Alarm> AddAlarmAsync(CreateAlarmDto createAlarm)
        {
            Alarm? newAlarm = mapper.Map<Alarm>(createAlarm);
            await eyeRepository.AddAlarmAsync(newAlarm);
            return await eyeRepository.GetAlarmByIdAsync(newAlarm.Id);
        }

        public async Task<Alarm> UpdateAlarmAsync(Alarm updateAlarm)
        {
            Alarm? editAlarm = mapper.Map<Alarm>(updateAlarm);
            await eyeRepository.UpdateAlarmAsync(editAlarm);
            return await eyeRepository.GetAlarmByIdAsync(editAlarm.Id);
        }
        public async Task DeleteAlarmByIdAsync(int alarmId) => await eyeRepository.DeleteAlarmByIdAsync(alarmId);
    }
}
