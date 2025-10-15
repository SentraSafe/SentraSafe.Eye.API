using EYEAPI.Contexts;
using EYEAPI.Exstensions;
using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace EYEAPI.Repositories
{
    public class EyeRepository(EyeContext eyeContext) : IEyeRepository
    {
        public async Task<List<MachineDto>> GetMachinesAsync(MachineSearchParamsDto searchParams) => 
            await eyeContext.Machines.WhereIfNotNull(searchParams.Name, machine => machine.Name == searchParams.Name)
            .WhereIfNotNull(searchParams.LocationId, machine => machine.Sublocation.Location.Id == searchParams.LocationId)
            .WhereIfNotNull(searchParams.SublocationId, machine => machine.Sublocation.Id == searchParams.SublocationId)
            .WhereIfNotNull(searchParams.MachineType, machine => machine.Type == searchParams.MachineType)
            .Include(machine => machine.Sublocation).ThenInclude(sublocation => sublocation.Location)
            .Select(machine => new MachineDto(machine))
            .ToListAsync();

        public async Task<Machine> AddMachineAsync(Machine newMachine) => (await eyeContext.Machines.AddAsync(newMachine)).Entity;

        public async Task<Sublocation> GetSublocationByIdAsync(int id) =>
            await eyeContext.Sublocations.WhereIfNotNull(id, sublocation => sublocation.Id == id)
            .Include(sublocation => sublocation.Location).FirstAsync();

        public async Task<List<Sublocation>> GetSublocationsByLocationAsync(int locationId) => 
            await eyeContext.Sublocations.WhereIfNotNull(locationId, sublocation => sublocation.Id == locationId)
            .ToListAsync();

        public async Task<List<LocationDto>> GetAllLocationsAsync() => await eyeContext.Locations
            .Include(location => location.Sublocations)
            .Select(location => new LocationDto(location))
            .ToListAsync();

        public async Task<Location> AddLocationAsync(Location newLocation) => (await eyeContext.Locations.AddAsync(newLocation)).Entity;
    
    }
}
