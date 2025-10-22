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
        Task<List<Machine>> GetMachinesAsync(MachineSearchParamsDto searchParams);
        Task AddMachineAsync(Machine newMachine);
        Task UpdateMachineAsync(Machine updateMachine);
        Task DeleteMachineByIdAsync(int machineId);
        Task<Machine?> GetMachineByIdAsync(int machineId);
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

        Task UpdateEventLogAsync(EventLog log);
        Task<EventLog?> GetEventLogByIdAsync(int logId);
        #endregion

        Task<Alarm?> GetAlarmByIdAsync(int alarmId);
        Task AddSublocationsAsync(List<Sublocation> newSublocations);
        Task<List<EventLog>> GetEventLogsAsync(EventLogSearchParamsDto? searchParams);
        Task AddEventLogsAsync(List<EventLog> eventLogs);
        Task AddEventLogAsync(EventLog newLog);
        Task AddMachineMetaDataAsync(MachineMetaData metaData);
    }
}
