using AutoMapper;
using EYEAPI.Contexts;
using EYEAPI.Exstensions;
using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Dtos.LogDtos;
using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Dtos.SublocationDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;
using Microsoft.EntityFrameworkCore;


namespace EYEAPI.Repositories
{
    public class EyeRepository(EyeContext eyeContext, IMapper mapper) : IEyeRepository
    {
        #region Machine
        public async Task<List<Machine>> GetMachinesAsync(MachineSearchParamsDto searchParams) =>
            await eyeContext.Machines.WhereIfNotNull(searchParams.Name, machine => machine.Name.Contains(searchParams.Name))
            .WhereIfNotNull(searchParams.LocationId, machine => machine.Sublocation.Location.Id == searchParams.LocationId)
            .WhereIfNotNull(searchParams.SublocationId, machine => machine.Sublocation.Id == searchParams.SublocationId)
            .WhereIfNotNull(searchParams.MachineType, machine => machine.Type == searchParams.MachineType)
            .Include(machine => machine.Sublocation).ThenInclude(sublocation => sublocation.Location)
            .ToListAsync();
        public async Task<Machine?> GetMachineByIdAsync(int machineId) =>
            await eyeContext.Machines.WhereIfNotNull(machineId, machine => machine.Id == machineId)
            .Include(machine => machine.Sublocation).ThenInclude(sublocation => sublocation.Location)
            .FirstOrDefaultAsync();
        public async Task AddMachineAsync(Machine newMachine)
        {
            await eyeContext.Machines.AddAsync(newMachine);
            await eyeContext.SaveChangesAsync();
        }
        public async Task UpdateMachineAsync(Machine updateMachine)
        {
            eyeContext.Machines.Update(updateMachine);
            await eyeContext.SaveChangesAsync();
        }
        public async Task DeleteMachineByIdAsync(int machineId)
        {
            Machine? machine = await eyeContext.Machines.WhereIfNotNull(machineId, machine => machine.Id == machineId)
                .FirstOrDefaultAsync();
            eyeContext.Remove(machine!);
            await eyeContext.SaveChangesAsync();
        }
        #endregion

        #region Sublocation
        public async Task<Sublocation> GetSublocationByIdAsync(int id) =>
            await eyeContext.Sublocations.WhereIfNotNull(id, sublocation => sublocation.Id == id)
            .Include(sublocation => sublocation.Location).FirstAsync();
        public async Task<List<Sublocation>> GetSublocationsByLocationAsync(int locationId) =>
            await eyeContext.Sublocations.WhereIfNotNull(locationId, sublocation => sublocation.Location.Id == locationId)
            .ToListAsync();
        public async Task AddSublocationAsync(Sublocation newSublocation)
        {
            await eyeContext.Sublocations.AddAsync(newSublocation);
            await eyeContext.SaveChangesAsync();
        }
        public async Task UpdateSublocationAsync(Sublocation updateSublocation)
        {
            eyeContext.Sublocations.Update(updateSublocation);
            await eyeContext.SaveChangesAsync();
        }
        public async Task DeleteSublocationByIdAsync(int sublocationId)
        {
            eyeContext.Sublocations.Remove(await GetSublocationByIdAsync(sublocationId));
            await eyeContext.SaveChangesAsync();
        }
        #endregion

        #region Location
        public async Task<List<Location>> GetAllLocationsAsync() => await eyeContext.Locations
            .Include(location => location.Sublocations)
            .ToListAsync();
        public async Task<Location?> GetLocationByIdAsync(int locationId) => await eyeContext.Locations
            .Include(location => location.Sublocations)
            .FirstOrDefaultAsync(location => location.Id == locationId);
        public async Task AddLocationAsync(Location newLocation)
        {
            await eyeContext.Locations.AddAsync(newLocation);
            await eyeContext.SaveChangesAsync();
        }
        public async Task UpdateLocationAsync(Location location)
        {
            eyeContext.Locations.Update(location);
            await eyeContext.SaveChangesAsync();
        }
        public async Task DeleteLocationByIdAsync(int locationId)
        {
            eyeContext.Locations.Remove(await GetLocationByIdAsync(locationId));
            await eyeContext.SaveChangesAsync();
        }
        #endregion

        #region Alarm
        public async Task<List<Alarm>> GetAlarmsAsync(AlarmSearchParamsDto searchParams) =>
            await eyeContext.Alarms.WhereIfNotNull(searchParams.Id, alarm => alarm.Id == searchParams.Id)
            .WhereIfNotNull(searchParams.Title, alarm => alarm.Title == searchParams.Title)
            .WhereIfNotNull(searchParams.MachineId, alarm => alarm.MachineId == searchParams.MachineId)
            .WhereIfNotNull(searchParams.ValueType, alarm => alarm.ValueType == searchParams.ValueType)
            .WhereIfNotNull(searchParams.Severity, alarm => alarm.Severity == searchParams.Severity)
            .Include(alarm => alarm.Machine)
            .ToListAsync();
        public async Task<Alarm?> GetAlarmByIdAsync(int alarmId) => await eyeContext.Alarms
           .FirstOrDefaultAsync(alarm => alarm.Id == alarmId);
        public async Task AddAlarmAsync(Alarm newAlarm)
        {
            await eyeContext.Alarms.AddAsync(newAlarm);
            await eyeContext.SaveChangesAsync();
        }
        public async Task UpdateAlarmAsync(Alarm updateAlarm)
        {
            eyeContext.Alarms.Update(updateAlarm);
            await eyeContext.SaveChangesAsync();
        }
        public async Task DeleteAlarmByIdAsync(int alarmId)
        {
            eyeContext.Alarms.Remove(await GetAlarmByIdAsync(alarmId));
            await eyeContext.SaveChangesAsync();
        }
        #endregion

        #region Log

        public async Task<List<Log>> GetLogsAsync(LogSearchParamsDto? searchParams) =>
            await eyeContext.Logs.WhereIfNotNull(searchParams?.AlarmId, log => log.AlarmId == searchParams.AlarmId)
                .WhereIfNotNull(searchParams?.IsHandled, log => log.IsHandled == searchParams.IsHandled)
                .WhereIfNotNull(searchParams?.Severity, log => log.Severity >= searchParams.Severity)
                .WhereIfNotNull(searchParams?.TimeStampFrom, log => log.TimeStamp >= searchParams.TimeStampFrom)
                .WhereIfNotNull(searchParams?.TimeStampTo, log => log.TimeStamp <= searchParams.TimeStampTo)
                .WhereIfNotNull(searchParams?.HandleTimeFrom, log => log.HandleTime >= searchParams.HandleTimeFrom)
                .WhereIfNotNull(searchParams?.HandleTimeTo, log => log.HandleTime <= searchParams.HandleTimeTo)
                .OrderBy(x => x.IsHandled)
                .ThenByDescending(x => x.Severity)
                .ThenByDescending(x => x.TimeStamp).ToListAsync();

        public async Task<Log?> GetLogByIdAsync(int logId) => await eyeContext.Logs
            .FirstAsync(log => logId == log.Id);
        public async Task AddLogAsync(Log newLog)
        {
            await eyeContext.Logs.AddAsync(newLog);
            await eyeContext.SaveChangesAsync();
        }
        public async Task UpdateLogAsync(Log log)
        {
            
            eyeContext.Logs.Update(log);
            await eyeContext.SaveChangesAsync();
        }
        public async Task DeleteLogByIdAsync(int logId)
        {
            eyeContext.Logs.Remove(await GetLogByIdAsync(logId));
            await eyeContext.SaveChangesAsync();
        }
        #endregion
    }
}
