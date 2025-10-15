using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Repositories;

namespace EYEAPI.Services.LocationService
{
    public class LocationService(IEyeRepository eyeRepository) : ILocationService
    {
        public async Task<List<LocationDto>> GetAllLocationsAsync() => await eyeRepository.GetAllLocationsAsync();

        public async Task<LocationDto> AddLocationAsync(CreateLocationDto createLocation)
        {
            Location location = new Location() {
                Name = createLocation.Name
            };
            Location newLocation = await eyeRepository.AddLocationAsync(location);
            return new LocationDto(newLocation);
        }
    }
}
