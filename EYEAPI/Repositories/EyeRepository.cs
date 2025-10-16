using AutoMapper;
using EYEAPI.Contexts;
using EYEAPI.Exstensions;
using EYEAPI.Models.Dtos.LocationDtos;
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
            await eyeContext.Machines.WhereIfNotNull(searchParams.Name, machine => machine.Name == searchParams.Name)
            .WhereIfNotNull(searchParams.LocationId, machine => machine.Sublocation.Location.Id == searchParams.LocationId)
            .WhereIfNotNull(searchParams.SublocationId, machine => machine.Sublocation.Id == searchParams.SublocationId)
            .WhereIfNotNull(searchParams.MachineType, machine => machine.Type == searchParams.MachineType)
            .Include(machine => machine.Sublocation).ThenInclude(sublocation => sublocation.Location)
            .Select(machine => new MachineDto(machine))
            .ToListAsync();

        public async Task<Machine> AddMachineAsync(Machine newMachine) {
            await eyeContext.Machines.AddAsync(newMachine);
            await eyeContext.SaveChangesAsync();
            return newMachine;
        }
        #endregion

        #region Sublocation
        public async Task<Sublocation> GetSublocationByIdAsync(int id) =>
            await eyeContext.Sublocations.WhereIfNotNull(id, sublocation => sublocation.Id == id)
            .Include(sublocation => sublocation.Location).FirstAsync();

        public async Task<List<Sublocation>> GetSublocationsByLocationAsync(int locationId) =>
            await eyeContext.Sublocations.WhereIfNotNull(locationId, sublocation => sublocation.Id == locationId)
            .ToListAsync();
        #endregion

        #region Location
        public async Task<List<Location>> GetAllLocationsAsync() => await eyeContext.Locations
            .Include(location => location.Sublocations)
            .ToListAsync();
        private async Task<Location?> GetLocationById(int locationId) => await eyeContext.Locations
            .Include(location => location.Sublocations)
            .FirstOrDefaultAsync();
        public async Task<Location> AddLocationAsync(Location newLocation) => (await eyeContext.Locations.AddAsync(newLocation)).Entity;

        public async Task DeleteLocationByIdAsync(int locationId)
        {
            Location? location = await eyeContext.Locations.WhereIfNotNull(locationId, location => location.Id == locationId)
                .FirstOrDefaultAsync();
            eyeContext.Remove(location!);
            await eyeContext.SaveChangesAsync();
        }

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
            return await GetLocationById(locationDto.Id);
        }
        #endregion

        #region Alarm

        #endregion
    }
}
