using AutoMapper;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.MachineDtos
{
    public class MachineDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Sublocation { get; set; }
        public MachineType MachineType { get; set; }
        public string? Status { get; set; }
    }
}
