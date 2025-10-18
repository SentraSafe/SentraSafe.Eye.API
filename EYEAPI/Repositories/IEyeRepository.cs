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
        Task AddSublocationAsync(Sublocation createSublocation);
        Task UpdateSublocationAsync(Sublocation updateSublocation);
        #endregion

        #region Location
        Task<List<Location>> GetAllLocationsAsync();
        Task<Location?> GetLocationByIdAsync(int locationId);
        Task AddLocationAsync(Location newLocation);
        Task UpdateLocationAsync(Location location);
        Task DeleteLocationByIdAsync(int locationId);
        #endregion

        #region Alarm
        Task<List<Alarm>> GetAlarmsAsync(AlarmSearchParamsDto searchParams);
        Task AddAlarmAsync(Alarm newAlarm);
        Task UpdateAlarmAsync(Alarm updateAlarm);
        Task DeleteAlarmByIdAsync(int alarmId);
        Task DeleteSublocationByIdAsync(int sublocationId);
        #endregion

        #region Logs
        Task AddLogAsync(Log newLogs);
        Task UpdateLogAsync(Log log);
        Task DeleteLogByIdAsync(int logId);
        Task<Log?> GetLogByIdAsync(int logId);
        #endregion

        Task<List<Log>> GetLogsAsync(LogSearchParamsDto searchParams);
        Task<Alarm> GetAlarmByIdAsync(int alarmId);
    }
}
