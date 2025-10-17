using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Dtos.SublocationDtos;
using EYEAPI.Models.Entities;

namespace EYEAPI.Repositories
{
    public interface IEyeRepository
    {
        Task<List<MachineDto>> GetMachinesAsync(MachineSearchParamsDto searchParams);
        Task<Machine> AddMachineAsync(Machine newMachine);
        Task<MachineDto> UpdateMachineAsync(Machine updateMachine);
        Task DeleteMachineByIdAsync(int machineId);

        Task<Sublocation> GetSublocationByIdAsync(int id);
        Task<List<Sublocation>> GetSublocationsByLocationAsync(int locationId);
        Task<SublocationDto> AddSublocationAsync(CreateSublocationDto createSublocation);

        Task<List<Location>> GetAllLocationsAsync();
        Task<Location> AddLocationAsync(Location newLocation);
        Task<Location> UpdateLocationAsync(LocationDto locationDto);
        Task DeleteLocationByIdAsync(int locationId);

        Task<List<Alarm>> GetAlarmsAsync(AlarmSearchParamsDto searchParams);
        Task<Alarm> AddAlarmAsync(Alarm newAlarm);
        Task<Alarm> UpdateAlarmAsync(Alarm updateAlarm);
        Task DeleteAlarmByIdAsync(int alarmId);
        Task DeleteSublocationByIdAsync(int sublocationId);
        Task<Sublocation> UpdateSublocationAsync(Sublocation updateSublocation);
    }
}
