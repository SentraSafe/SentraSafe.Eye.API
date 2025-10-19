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
        public async Task<MachineDto> GetMachineAsync(int id) => mapper.Map<MachineDto>(await eyeRepository.GetMachineByIdAsync(id));

        public async Task<MachineDto?> AddMachineAsync(CreateMachineDto createMachine)
        {
            Machine newMachine = mapper.Map<Machine>(createMachine);
            await eyeRepository.AddMachineAsync(newMachine);
            return mapper.Map<MachineDto>(await eyeRepository.GetMachineByIdAsync(newMachine.Id));
        }

        public async Task<MachineDto?> UpdateMachineAsync(UpdateMachineDto updateMachine)
        {
            Machine editMachine = mapper.Map<Machine>(updateMachine);
            await eyeRepository.UpdateMachineAsync(editMachine);
            return mapper.Map<MachineDto>(await eyeRepository.GetMachineByIdAsync(editMachine.Id));
        }
        public async Task DeleteMachineByIdAsync(int machineId) => await eyeRepository.DeleteMachineByIdAsync(machineId);
    }
}
