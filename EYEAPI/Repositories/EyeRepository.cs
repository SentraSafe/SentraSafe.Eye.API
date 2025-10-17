using AutoMapper;
using EYEAPI.Contexts;
using EYEAPI.Exstensions;
using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Dtos.LogDtos;
using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Dtos.SublocationDtos;
using EYEAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace EYEAPI.Repositories
{
    public class EyeRepository(EyeContext eyeContext, IMapper mapper) : IEyeRepository
    {
        #region Machine
        public async Task<List<MachineDto>> GetMachinesAsync(MachineSearchParamsDto searchParams) =>
            await eyeContext.Machines.WhereIfNotNull(searchParams.Name, machine => machine.Name.Contains(searchParams.Name))
            .WhereIfNotNull(searchParams.LocationId, machine => machine.Sublocation.Location.Id == searchParams.LocationId)
            .WhereIfNotNull(searchParams.SublocationId, machine => machine.Sublocation.Id == searchParams.SublocationId)
            .WhereIfNotNull(searchParams.MachineType, machine => machine.Type == searchParams.MachineType)
            .Include(machine => machine.Sublocation).ThenInclude(sublocation => sublocation.Location)
            .Select(machine => new MachineDto(machine))
            .ToListAsync();
        public async Task<MachineDto> GetMachinesByIdAsync(int machineId) =>
            await eyeContext.Machines.WhereIfNotNull(machineId, machine => machine.Id == machineId)
            .Include(machine => machine.Sublocation).ThenInclude(sublocation => sublocation.Location)
            .Select(machine => new MachineDto(machine))
            .FirstOrDefaultAsync();
        public async Task<Machine> AddMachineAsync(Machine newMachine) 
        {
            await eyeContext.Machines.AddAsync(newMachine);
            await eyeContext.SaveChangesAsync();
            return newMachine;
        }
        public async Task<MachineDto> UpdateMachineAsync(Machine updateMachine)
        {

            eyeContext.Machines.Update(updateMachine);
            await eyeContext.SaveChangesAsync();
            return await GetMachinesByIdAsync(updateMachine.Id);
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
            await eyeContext.Sublocations.WhereIfNotNull(locationId, sublocation => sublocation.Id == locationId)
            .ToListAsync();
        public async Task<Sublocation> AddSublocationAsync(Sublocation newSublocation) => (await eyeContext.Sublocations.AddAsync(newSublocation)).Entity;
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
        private async Task<Location?> GetLocationByIdAsync(int locationId) => await eyeContext.Locations
            .Include(location => location.Sublocations)
            .FirstOrDefaultAsync();
        public async Task<Location> AddLocationAsync(Location newLocation) => (await eyeContext.Locations.AddAsync(newLocation)).Entity;
        public async Task<Location> UpdateLocationAsync(LocationDto locationDto)
        {
            var location = new Location
            {
                Id = locationDto.Id,
                Name = locationDto.Name,
                Sublocations = (ICollection<Sublocation>)locationDto.Sublocations
            };
            eyeContext.Locations.Update(location);
            await eyeContext.SaveChangesAsync();
            return await GetLocationByIdAsync(locationDto.Id);
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
            .WhereIfNotNull(searchParams.MachineType, alarm => alarm.MachineType == searchParams.MachineType)
            .WhereIfNotNull(searchParams.Severity, alarm => alarm.Severity == searchParams.Severity)
            .Include(alarm => alarm.Machine)
            .ToListAsync();
        private async Task<Alarm?> GetAlarmByIdAsync(int alarmId) => await eyeContext.Alarms
           .FirstOrDefaultAsync();
        public async Task<Alarm> AddAlarmAsync(Alarm newAlarm) => (await eyeContext.Alarms.AddAsync(newAlarm)).Entity;
        public async Task<Alarm> UpdateAlarmAsync(Alarm updateAlarm)
        {
            eyeContext.Alarms.Update(updateAlarm);
            await eyeContext.SaveChangesAsync();
            return await GetAlarmByIdAsync(updateAlarm.Id);
        }
        public async Task DeleteAlarmByIdAsync(int alarmId)
        {
            eyeContext.Alarms.Remove(await GetAlarmByIdAsync(alarmId));
            await eyeContext.SaveChangesAsync();
        }
        #endregion

        #region Log

        public async Task<List<LogDto>> GetLogsAsync(LogSearchParamsDto searchParams) =>
            await eyeContext.Logs.WhereIfNotNull(searchParams.,log => log.AlarmId == searchParams)

        private async Task<Log?> GetLogByIdAsync(int logId) => await eyeContext.Logs
            .FirstAsync(log => logId == log.Id);
        public async Task<Log> AddLogAsync(Log newLog) => (await eyeContext.Logs.AddAsync(newLog)).Entity;
        public async Task<Log> UpdateLogAsync(Log log)
        {
            
            eyeContext.Logs.Update(log);
            await eyeContext.SaveChangesAsync();
            return await GetLogByIdAsync(log.Id);
        }
        public async Task DeleteLogByIdAsync(int locationId)
        {
            eyeContext.Locations.Remove(await GetLocationByIdAsync(locationId));
            await eyeContext.SaveChangesAsync();
        }
        
        #endregion
    }
}
