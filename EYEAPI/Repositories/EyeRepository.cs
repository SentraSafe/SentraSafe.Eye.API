using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EYEAPI.Contexts;
using EYEAPI.Exstensions;
using EYEAPI.Models.Dtos.AlarmDtos; // Only for search params DTOs
using EYEAPI.Models.Dtos.LogDtos; // Only for search params DTOs
using EYEAPI.Models.Dtos.MachineDtos; // Only for search params DTOs
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EYEAPI.Repositories
{
    public class EyeRepository(EyeContext eyeContext, IMapper mapper) : IEyeRepository
    {
        #region Machine

        public async Task<List<Machine>> GetMachinesAsync(MachineSearchParamsDto searchParams)
        {
            DateTime thresholdDate = DateTime.Now.AddMinutes(-5);
            List<Machine> machines = await eyeContext.Machines.WhereIfNotNull(searchParams.Name, machine => machine.Name.Contains(searchParams.Name))
                .WhereIfNotNull(searchParams.LocationId, machine => machine.Sublocation.Location.Id == searchParams.LocationId)
                .WhereIfNotNull(searchParams.SublocationId, machine => machine.Sublocation.Id == searchParams.SublocationId)
                .WhereIfNotNull(searchParams.Type, machine => machine.Type == searchParams.Type)
                .Include(machine => machine.Sublocation).ThenInclude(sublocation => sublocation.Location)
                .Include(x => x.EventLogs)
                .Select(x => new Machine()
                {
                    Name = x.Name,
                    Sublocation = x.Sublocation,
                    Type = x.Type,
                    SublocationId = x.SublocationId,
                    Id = x.Id,
                    Status = x.EventLogs != null && x.EventLogs.OrderByDescending(y => y.TimeCreated).First().TimeCreated < thresholdDate ? "Healthy" : "Unhealty"
                })
                .ToListAsync();
            return machines;
        }
        
        public async Task<Machine?> GetMachineByIdAsync(int machineId)
        {
            DateTime thresholdDate = DateTime.Now.AddMinutes(-5);

            return await eyeContext.Machines
                .AsNoTracking()
                .Include(m => m.Sublocation)
                .ThenInclude(s => s.Location)
                .Include(x => x.EventLogs)
                .Select(x => new Machine()
                {
                    Name = x.Name,
                    Sublocation = x.Sublocation,
                    Type = x.Type,
                    SublocationId = x.SublocationId,
                    Id = x.Id,
                    Status = x.EventLogs != null && x.EventLogs.OrderByDescending(y => y.TimeCreated).First().TimeCreated < thresholdDate ? "Healthy" : "Unhealty"
                })
                .FirstOrDefaultAsync(m => m.Id == machineId);
        }

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
            var machine = await eyeContext.Machines.FindAsync([machineId]);
            if (machine is null) return;
            eyeContext.Machines.Remove(machine);
            await eyeContext.SaveChangesAsync();
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

        public async Task AddSublocationAsync(Sublocation newSublocation)
        {
            await eyeContext.Sublocations.AddAsync(newSublocation);
            await eyeContext.SaveChangesAsync();
        }

        public async Task AddSublocationsAsync(List<Sublocation> newSublocations)
        {
            await eyeContext.Sublocations.AddRangeAsync(newSublocations);
            await eyeContext.SaveChangesAsync();
        }

        public async Task UpdateSublocationAsync(Sublocation updateSublocation)
        {
            eyeContext.Sublocations.Update(updateSublocation);
            await eyeContext.SaveChangesAsync();
        }

        public async Task DeleteSublocationByIdAsync(int sublocationId)
        {
            var sublocation = await eyeContext.Sublocations.FindAsync([sublocationId]);
            if (sublocation is null) return;
            eyeContext.Sublocations.Remove(sublocation);
            await eyeContext.SaveChangesAsync();
        }

        #endregion

        #region Location

        public async Task<List<Location>> GetAllLocationsAsync() => await eyeContext.Locations
            .Include(location => location.Sublocations)
            .ThenInclude(sublocation => sublocation.Machines)
            .Select(x => new Location()
            {
                Id = x.Id,
                Name = x.Name,
                Sublocations = x.Sublocations,
                MachineCount = x.Sublocations.Sum(sublocation => sublocation.Machines.Count)
            })
            .ToListAsync();

        public async Task<Location?> GetLocationByIdAsync(int locationId) =>
            await eyeContext.Locations
                .AsNoTracking()
                .Include(l => l.Sublocations)
                .FirstOrDefaultAsync(l => l.Id == locationId);

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
            var location = await eyeContext.Locations.FindAsync([locationId]);
            if (location is null) return;
            eyeContext.Locations.Remove(location);
            await eyeContext.SaveChangesAsync();
        }

        #endregion

        #region Alarm

        public async Task<List<Alarm>> GetAlarmsAsync(AlarmSearchParamsDto searchParams) =>
            await eyeContext.Alarms.WhereIfNotNull(searchParams.Id, alarm => alarm.Id == searchParams.Id)
                .AsNoTracking()
                .WhereIfNotNull(searchParams.Title, alarm => alarm.Title == searchParams.Title)
                .WhereIfNotNull(searchParams.MachineId, alarm => alarm.MachineId == searchParams.MachineId)
                .WhereIfNotNull(searchParams.ValueType, alarm => alarm.ValueType == searchParams.ValueType)
                .WhereIfNotNull(searchParams.ValueType, alarm => alarm.MaximumValue <= searchParams.Value)
                .WhereIfNotNull(searchParams.Severity, alarm => alarm.Severity == searchParams.Severity)
                .ToListAsync();

        public async Task<Alarm?> GetAlarmByIdAsync(int alarmId) =>
            await eyeContext.Alarms
                .AsNoTracking()
                .Include(a => a.Machine)
                .FirstOrDefaultAsync(a => a.Id == alarmId);

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

        public async Task<EventLog?> GetEventLogByIdAsync(int logId) => await eyeContext.EventLogs
            .FirstAsync(log => logId == log.Id);

        public async Task AddEventLogAsync(EventLog newLog)
        {
            await eyeContext.EventLogs.AddAsync(newLog);
            await eyeContext.SaveChangesAsync();
        }

        public async Task UpdateEventLogAsync(EventLog log)
        {
            eyeContext.EventLogs.Update(log);
            await eyeContext.SaveChangesAsync();
        }

        public async Task<List<EventLog>> GetEventLogsAsync(EventLogSearchParamsDto? searchParams)
        {
            return await eyeContext.EventLogs.WhereIfNotNull(searchParams?.AlarmId, log => log.AlarmId == searchParams.AlarmId)
                .WhereIfNotNull(searchParams?.MachineIds, log => searchParams.MachineIds.Contains(log.MachineId))
                .WhereIfNotNull(searchParams?.IsHandled, log => log.IsHandled == searchParams.IsHandled)
                .WhereIfNotNull(searchParams?.HandledBy, l => l.HandledBy.Contains(searchParams.HandledBy))
                .WhereIfNotNull(searchParams?.Severity, log => log.Severity >= searchParams.Severity)
                .WhereIfNotNull(searchParams?.TimeStampFrom, log => log.TimeCreated >= searchParams.TimeStampFrom)
                .WhereIfNotNull(searchParams?.TimeStampTo, log => log.TimeCreated <= searchParams.TimeStampTo)
                .WhereIfNotNull(searchParams?.HandledFrom, log => log.HandledAt >= searchParams.HandledFrom)
                .WhereIfNotNull(searchParams?.HandledTo, log => log.HandledAt <= searchParams.HandledTo)
                .WhereIfNotNull(searchParams?.AlarmIdNotNull, log => log.AlarmId != null)
                .WhereIfNotNull(searchParams?.LocationId, log => log.Machine.Sublocation.Location.Id == searchParams.LocationId)
                .OrderBy(x => x.IsHandled)
                .ThenByDescending(x => x.Severity)
                .ThenByDescending(x => x.TimeCreated).ToListAsync();
        }

        public async Task AddEventLogsAsync(List<EventLog> eventLogs)
        {
            await eyeContext.EventLogs.AddRangeAsync(eventLogs);
            await eyeContext.SaveChangesAsync();
        }

        #endregion
    }
}