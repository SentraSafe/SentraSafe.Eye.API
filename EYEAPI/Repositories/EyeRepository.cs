using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EYEAPI.Contexts;
using EYEAPI.Exstensions;
using EYEAPI.Models.Dtos.AlarmDtos;       // Only for search params DTOs
using EYEAPI.Models.Dtos.LogDtos;         // Only for search params DTOs
using EYEAPI.Models.Dtos.MachineDtos;     // Only for search params DTOs
using EYEAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EYEAPI.Repositories
{
    public class EyeRepository : IEyeRepository
    {
        private readonly EyeContext eyeContext;

        public EyeRepository(EyeContext eyeContext)
        {
            this.eyeContext = eyeContext;
        }

        #region Machine

        public async Task<List<Machine>> GetMachinesAsync(MachineSearchParamsDto searchParams) =>
            await eyeContext.Machines
                .AsNoTracking()
                .WhereIfNotNull(searchParams.Name, m => EF.Functions.Like(m.Name, $"%{searchParams.Name}%"))
                .WhereIfNotNull(searchParams.LocationId, m => m.Sublocation != null && m.Sublocation.LocationId == searchParams.LocationId)
                .WhereIfNotNull(searchParams.SublocationId, m => m.SublocationId == searchParams.SublocationId)
                .WhereIfNotNull(searchParams.MachineType, m => m.Type == searchParams.MachineType)
                .Include(m => m.Sublocation)
                    .ThenInclude(s => s.Location)
                .ToListAsync();

        public async Task<Machine?> GetMachineByIdAsync(int machineId) =>
            await eyeContext.Machines
                .AsNoTracking()
                .Include(m => m.Sublocation)
                    .ThenInclude(s => s.Location)
                .FirstOrDefaultAsync(m => m.Id == machineId);

        public async Task<Machine> AddMachineAsync(Machine newMachine)
        {
            await eyeContext.Machines.AddAsync(newMachine);
            await eyeContext.SaveChangesAsync();
            return newMachine;
        }

        public async Task<Machine?> UpdateMachineAsync(Machine updateMachine)
        {
            // If you prefer fetch-and-patch, do it here. For now we trust the incoming entity.
            eyeContext.Machines.Attach(updateMachine);
            eyeContext.Entry(updateMachine).State = EntityState.Modified;
            await eyeContext.SaveChangesAsync();
            return await GetMachineByIdAsync(updateMachine.Id);
        }

        public async Task<bool> DeleteMachineByIdAsync(int machineId)
        {
            var machine = await eyeContext.Machines.FindAsync([machineId]);
            if (machine is null) return false;
            eyeContext.Machines.Remove(machine);
            await eyeContext.SaveChangesAsync();
            return true;
        }

        #endregion

        #region Sublocation

        public async Task<Sublocation?> GetSublocationByIdAsync(int id) =>
            await eyeContext.Sublocations
                .AsNoTracking()
                .Include(s => s.Location)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<List<Sublocation>> GetSublocationsByLocationAsync(int locationId) =>
            await eyeContext.Sublocations
                .AsNoTracking()
                .Where(s => s.LocationId == locationId)
                .ToListAsync();

        public async Task<Sublocation> AddSublocationAsync(Sublocation newSublocation)
        {
            await eyeContext.Sublocations.AddAsync(newSublocation);
            await eyeContext.SaveChangesAsync();
            return newSublocation;
        }

        public async Task<Sublocation?> UpdateSublocationAsync(Sublocation updateSublocation)
        {
            eyeContext.Sublocations.Attach(updateSublocation);
            eyeContext.Entry(updateSublocation).State = EntityState.Modified;
            await eyeContext.SaveChangesAsync();
            return await GetSublocationByIdAsync(updateSublocation.Id);
        }

        public async Task<bool> DeleteSublocationByIdAsync(int sublocationId)
        {
            var sublocation = await eyeContext.Sublocations.FindAsync([sublocationId]);
            if (sublocation is null) return false;
            eyeContext.Sublocations.Remove(sublocation);
            await eyeContext.SaveChangesAsync();
            return true;
        }

        #endregion

        #region Location

        public async Task<List<Location>> GetAllLocationsAsync() =>
            await eyeContext.Locations
                .AsNoTracking()
                .Include(l => l.Sublocations)
                .ToListAsync();

        public async Task<Location?> GetLocationByIdAsync(int locationId) =>
            await eyeContext.Locations
                .AsNoTracking()
                .Include(l => l.Sublocations)
                .FirstOrDefaultAsync(l => l.Id == locationId);

        public async Task<Location> AddLocationAsync(Location newLocation)
        {
            await eyeContext.Locations.AddAsync(newLocation);
            await eyeContext.SaveChangesAsync();
            return newLocation;
        }

        public async Task<Location?> UpdateLocationAsync(Location updateLocation)
        {
            eyeContext.Locations.Attach(updateLocation);
            eyeContext.Entry(updateLocation).State = EntityState.Modified;
            await eyeContext.SaveChangesAsync();
            return await GetLocationByIdAsync(updateLocation.Id);
        }

        public async Task<bool> DeleteLocationByIdAsync(int locationId)
        {
            var location = await eyeContext.Locations.FindAsync([locationId]);
            if (location is null) return false;
            eyeContext.Locations.Remove(location);
            await eyeContext.SaveChangesAsync();
            return true;
        }

        #endregion

        #region Alarm

        public async Task<List<Alarm>> GetAlarmsAsync(AlarmSearchParamsDto searchParams) =>
            await eyeContext.Alarms
                .AsNoTracking()
                .WhereIfNotNull(searchParams.Id, a => a.Id == searchParams.Id)
                .WhereIfNotNull(searchParams.Title, a => a.Title == searchParams.Title)
                .WhereIfNotNull(searchParams.MachineId, a => a.MachineId == searchParams.MachineId)
                .WhereIfNotNull(searchParams.MachineType, a => a.MachineType == searchParams.MachineType)
                .WhereIfNotNull(searchParams.Severity, a => a.Severity == searchParams.Severity)
                .Include(a => a.Machine)
                .ToListAsync();

        public async Task<Alarm?> GetAlarmByIdAsync(int alarmId) =>
            await eyeContext.Alarms
                .AsNoTracking()
                .Include(a => a.Machine)
                .FirstOrDefaultAsync(a => a.Id == alarmId);

        public async Task<Alarm> AddAlarmAsync(Alarm newAlarm)
        {
            await eyeContext.Alarms.AddAsync(newAlarm);
            await eyeContext.SaveChangesAsync();
            return newAlarm;
        }

        public async Task<Alarm?> UpdateAlarmAsync(Alarm updateAlarm)
        {
            eyeContext.Alarms.Attach(updateAlarm);
            eyeContext.Entry(updateAlarm).State = EntityState.Modified;
            await eyeContext.SaveChangesAsync();
            return await GetAlarmByIdAsync(updateAlarm.Id);
        }

        public async Task<bool> DeleteAlarmByIdAsync(int alarmId)
        {
            var alarm = await eyeContext.Alarms.FindAsync([alarmId]);
            if (alarm is null) return false;
            eyeContext.Alarms.Remove(alarm);
            await eyeContext.SaveChangesAsync();
            return true;
        }

        #endregion

        #region Log

        public async Task<List<Log>> GetLogsAsync(LogSearchParamsDto searchParams) =>
            await eyeContext.Logs
                .AsNoTracking()
                .WhereIfNotNull(searchParams.Time, l => l.TimeStamp >= searchParams.Time)
                .WhereIfNotNull(searchParams.Severity, l => l.Severity == searchParams.Severity)
                .WhereIfNotNull(searchParams.MachineID, l => l.MachineId == searchParams.MachineID)
                .WhereIfNotNull(searchParams.AlarmId, l => l.AlarmId == searchParams.AlarmId)
                .WhereIfNotNull(searchParams.IsHandled, l => l.IsHandled == searchParams.IsHandled)
                .WhereIfNotNull(searchParams.HandledBy, l => l.HandledBy.Contains(searchParams.HandledBy))
                .Include(l => l.Machine)
                .Include(l => l.Alarm)
                .ToListAsync();

        public async Task<Log?> GetLogByIdAsync(int logId) =>
            await eyeContext.Logs
                .AsNoTracking()
                .Include(l => l.Machine)
                .Include(l => l.Alarm)
                .FirstOrDefaultAsync(l => l.Id == logId);

        public async Task<Log> AddLogAsync(Log newLog)
        {
            await eyeContext.Logs.AddAsync(newLog);
            await eyeContext.SaveChangesAsync();
            return newLog;
        }

        public async Task<Log?> UpdateLogAsync(Log updateLog)
        {
            eyeContext.Logs.Attach(updateLog);
            eyeContext.Entry(updateLog).State = EntityState.Modified;
            await eyeContext.SaveChangesAsync();
            return await GetLogByIdAsync(updateLog.Id);
        }

        public async Task<bool> DeleteLogByIdAsync(int logId)
        {
            var log = await eyeContext.Logs.FindAsync([logId]);
            if (log is null) return false;
            eyeContext.Logs.Remove(log);
            await eyeContext.SaveChangesAsync();
            return true;
        }

        #endregion
    }
}
