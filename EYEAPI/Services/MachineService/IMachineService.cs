using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Enums;

namespace EYEAPI.Services.MachineService
{
    public interface IMachineService
    {
        Task<List<MachineDto>> GetMachinesAsync(MachineSearchParamsDto searchParams);
        Task<MachineDto> AddMachineAsync(CreateMachineDto createMachine);
    }
}
