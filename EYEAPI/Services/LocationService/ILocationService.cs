using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Entities;

namespace EYEAPI.Services.LocationService
{
    public interface ILocationService
    {
        Task<LocationDto> AddLocationAsync(CreateLocationDto createLocation);
        Task DeleteLocationByIdAsync(int locationId);
        Task<List<LocationDto>> GetAllLocationsAsync();
        Task<LocationDto> UpdateLocationAsync(LocationDto newLocation);
    }
}
