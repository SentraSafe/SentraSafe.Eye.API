using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Entities;

namespace EYEAPI.Repositories
{
    public interface IEyeRepository
    {
        Task<List<MachineDto>> GetMachinesAsync(MachineSearchParamsDto searchParams);
        Task<Machine> AddMachineAsync(Machine newMachine);
        Task<Sublocation> GetSublocationByIdAsync(int id);
        Task<List<Sublocation>> GetSublocationsByLocationAsync(int locationId);
        Task<List<Location>> GetAllLocationsAsync();
        Task<Location> AddLocationAsync(Location newLocation);
        Task DeleteLocationByIdAsync(int locationId);
        Task<Location> UpdateLocationAsync(LocationDto locationDto);
        Task<MachineDto> UpdateMachineAsync(Machine updateMachine);
        Task<List<Alarm>> GetAlarmsAsync(AlarmSearchParamsDto searchParams);
    }
}
