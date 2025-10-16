using AutoMapper;
using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Repositories;

namespace EYEAPI.Services.LocationService
{
    public class LocationService(IEyeRepository eyeRepository, IMapper mapper) : ILocationService
    {
        public async Task<List<LocationDto>> GetAllLocationsAsync() => mapper.Map<List<LocationDto>>(await eyeRepository.GetAllLocationsAsync());

        public async Task<LocationDto> AddLocationAsync(CreateLocationDto createLocation)
        {
            Location location = new Location() {
                Name = createLocation.Name
            };
            Location newLocation = await eyeRepository.AddLocationAsync(location);
            return mapper.Map<LocationDto>(newLocation);
        }

        public async Task DeleteLocationByIdAsync(int locationId) => await eyeRepository.DeleteLocationByIdAsync(locationId);
        public async Task<LocationDto> UpdateLocationAsync(LocationDto location) => mapper.Map<LocationDto>(await eyeRepository.UpdateLocationAsync(location));
    }
}
