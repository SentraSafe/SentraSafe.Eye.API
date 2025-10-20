using AutoMapper;
using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Dtos.LogDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Repositories;

namespace EYEAPI.Services.LocationService
{
    public class LocationService(IEyeRepository eyeRepository, IMapper mapper) : ILocationService
    {
        public async Task<List<LocationDto>> GetAllLocationsAsync() => mapper.Map<List<LocationDto>>(await eyeRepository.GetAllLocationsAsync());

        public async Task<LocationDto> AddLocationAsync(CreateLocationDto createLocation)
        {
            Location? newLocation = mapper.Map<Location>(createLocation);
            await eyeRepository.AddLocationAsync(newLocation);
            return mapper.Map<LocationDto>(await eyeRepository.GetLocationByIdAsync(newLocation.Id));
        }

        public async Task DeleteLocationByIdAsync(int locationId) => await eyeRepository.DeleteLocationByIdAsync(locationId);
        public async Task<LocationDto> UpdateLocationAsync(LocationDto newLocation)
        {
            Location location = mapper.Map<Location>(newLocation);
            await eyeRepository.UpdateLocationAsync(location);
            return mapper.Map<LocationDto>(await eyeRepository.GetLocationByIdAsync(location.Id));
        }
    }
}
