using AutoMapper;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.MachineDtos
{
    [AutoMap(typeof(Machine))]

    public class UpdateMachineDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MachineType Type { get; set; }
        public int SublocationId { get; set; }
    }
}
