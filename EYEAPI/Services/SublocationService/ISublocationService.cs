using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Entities;

namespace EYEAPI.Services.SublocationService
{
    public interface ISublocationService
    {
        Task<LocationDto> AddLocationAsync(CreateLocationDto createLocation);
        Task DeleteLocationByIdAsync(int locationId);
        Task<List<LocationDto>> GetAllLocationsAsync();
        Task<LocationDto> UpdateLocationAsync(LocationDto location);
    }
}
