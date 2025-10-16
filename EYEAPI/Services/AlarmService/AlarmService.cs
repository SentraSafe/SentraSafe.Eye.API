using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Repositories;
using MongoDB.Driver.Search;

namespace EYEAPI.Services.AlarmService
{
    public class AlarmService(IEyeRepository eyeRepository) : IAlarmService
    {
        public async Task<List<Alarm>> GetAlarmsAsync(AlarmSearchParamsDto searchParams) => await eyeRepository.GetAlarmsAsync(searchParams);
    }
}
