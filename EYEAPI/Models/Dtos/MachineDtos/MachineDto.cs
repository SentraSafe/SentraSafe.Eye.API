using AutoMapper;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.MachineDtos
{
    [AutoMap(typeof(Machine))]
    public class MachineDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Sublocation { get; set; }
        public MachineType MachineType { get; set; }

        public MachineDto(Machine machine)
        {
            Id = machine.Id;
            Name = machine.Name;
            Location = machine.Sublocation.Location.Name;
            Sublocation = machine.Sublocation.Name;
            MachineType = machine.Type;
        }
    }
}
