using AutoMapper;
using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;
using EYEAPI.Repositories;
using System.Runtime.CompilerServices;

namespace EYEAPI.Services.MachineService
{
    public class MachineService(IEyeRepository eyeRepository, IMapper mapper) : IMachineService
    {
        public async Task<List<MachineDto>> GetMachinesAsync(MachineSearchParamsDto searchParams) => mapper.Map<List<MachineDto>>(await eyeRepository.GetMachinesAsync(searchParams));

        public async Task<MachineDto> AddMachineAsync(CreateMachineDto createMachine)
        {
            Sublocation sublocation = await eyeRepository.GetSublocationByIdAsync(createMachine.SublocationId);
            Machine newMachine = await eyeRepository.AddMachineAsync(mapper.Map<Machine>(createMachine));
            return new MachineDto(newMachine);
        }

        public async Task<MachineDto> UpdateMachineAsync(UpdateMachineDto updateMachine)
        {
            Machine editMachine = mapper.Map<Machine>(updateMachine);
            return mapper.Map<MachineDto>(await eyeRepository.UpdateMachineAsync(editMachine));
        }
        public async Task DeleteMachineByIdAsync(int machineId) => await eyeRepository.DeleteMachineByIdAsync(machineId);
    }
}
