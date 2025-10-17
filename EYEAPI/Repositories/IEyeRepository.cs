using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Dtos.LogDtos;
using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Entities;

namespace EYEAPI.Repositories
{
    public interface IEyeRepository
    {
        #region Machine
        Task<List<Machine>> GetMachinesAsync(MachineSearchParamsDto searchParams);
        Task<Machine?> GetMachineByIdAsync(int machineId);
        Task<Machine> AddMachineAsync(Machine newMachine);
        Task<Machine?> UpdateMachineAsync(Machine updateMachine);
        Task<bool> DeleteMachineByIdAsync(int machineId);
        #endregion

        #region Sublocation
        Task<Sublocation?> GetSublocationByIdAsync(int id);
        Task<List<Sublocation>> GetSublocationsByLocationAsync(int locationId);
        Task<Sublocation> AddSublocationAsync(Sublocation newSublocation);
        Task<Sublocation?> UpdateSublocationAsync(Sublocation updateSublocation);
        Task<bool> DeleteSublocationByIdAsync(int sublocationId);
        #endregion

        #region Location
        Task<List<Location>> GetAllLocationsAsync();
        Task<Location?> GetLocationByIdAsync(int locationId);
        Task<Location> AddLocationAsync(Location newLocation);
        Task<Location?> UpdateLocationAsync(Location updateLocation);
        Task<bool> DeleteLocationByIdAsync(int locationId);
        #endregion

        #region Alarm
        Task<List<Alarm>> GetAlarmsAsync(AlarmSearchParamsDto searchParams);
        Task<Alarm?> GetAlarmByIdAsync(int alarmId);
        Task<Alarm> AddAlarmAsync(Alarm newAlarm);
        Task<Alarm?> UpdateAlarmAsync(Alarm updateAlarm);
        Task<bool> DeleteAlarmByIdAsync(int alarmId);
        #endregion

        #region Log
        Task<List<Log>> GetLogsAsync(LogSearchParamsDto searchParams);
        Task<Log?> GetLogByIdAsync(int logId);
        Task<Log> AddLogAsync(Log newLog);
        Task<Log?> UpdateLogAsync(Log updateLog);
        Task<bool> DeleteLogByIdAsync(int logId);
        #endregion
    }
}
