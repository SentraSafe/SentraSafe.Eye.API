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
            Location newLocation = await eyeRepository.AddLocationAsync(mapper.Map<Location>(createLocation));
            return mapper.Map<LocationDto>(newLocation);
        }

        public async Task DeleteLocationByIdAsync(int locationId) => await eyeRepository.DeleteLocationByIdAsync(locationId);
        public async Task<LocationDto> UpdateLocationAsync(LocationDto location) => mapper.Map<LocationDto>(await eyeRepository.UpdateLocationAsync(location));
    }
}
