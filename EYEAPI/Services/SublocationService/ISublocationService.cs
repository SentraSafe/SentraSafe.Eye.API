using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Dtos.SublocationDtos;
using EYEAPI.Models.Entities;

namespace EYEAPI.Services.SublocationService
{
    public interface ISublocationService
    {
        Task<List<Sublocation>> GetSublocationByLocationAsync(int locationId);
        Task<Sublocation> AddSublocationAsync(CreateSublocationDto createSublocation);
        Task DeleteSublocationByIdAsync(int sublocationId);
        Task<Sublocation> UpdateSublocationAsync(SublocationDto sublocation);
    }
}
