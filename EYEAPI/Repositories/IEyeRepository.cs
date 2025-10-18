using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Dtos.LogDtos;
using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Dtos.SublocationDtos;
using EYEAPI.Models.Entities;

namespace EYEAPI.Repositories
{
    public interface IEyeRepository
    {
        #region Machine
        Task<List<MachineDto>> GetMachinesAsync(MachineSearchParamsDto searchParams);
        Task AddMachineAsync(Machine newMachine);
        Task UpdateMachineAsync(Machine updateMachine);
        Task DeleteMachineByIdAsync(int machineId);
        Task<MachineDto?> GetMachineByIdAsync(int machineId);
        #endregion

        #region Sublocation
        Task<Sublocation> GetSublocationByIdAsync(int id);
        Task<List<Sublocation>> GetSublocationsByLocationAsync(int locationId);
        Task<Sublocation> AddSublocationAsync(Sublocation createSublocation);
        Task UpdateSublocationAsync(Sublocation updateSublocation);
        #endregion

        #region Location
        Task<List<Location>> GetAllLocationsAsync();
        Task<Location> AddLocationAsync(Location newLocation);
        Task<Location> UpdateLocationAsync(LocationDto locationDto);
        Task DeleteLocationByIdAsync(int locationId);
        #endregion

        #region Alarm
        Task<List<Alarm>> GetAlarmsAsync(AlarmSearchParamsDto searchParams);
        Task<Alarm> AddAlarmAsync(Alarm newAlarm);
        Task<Alarm> UpdateAlarmAsync(Alarm updateAlarm);
        Task DeleteAlarmByIdAsync(int alarmId);
        Task DeleteSublocationByIdAsync(int sublocationId);
        #endregion

        #region Logs
        Task<Log> AddLogAsync(Log newLogs);
        Task<Log> UpdateLogAsync(Log log);
        Task DeleteLogByIdAsync(int locationId);
        #endregion
    }
}
