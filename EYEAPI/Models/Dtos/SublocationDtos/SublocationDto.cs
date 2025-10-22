using EYEAPI.Models.Entities;
using AutoMapper;
using EYEAPI.Models.Dtos.MachineDtos;

namespace EYEAPI.Models.Dtos.SublocationDtos
{
    [AutoMap(typeof(Sublocation))]
    public class SublocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public List<MachineDto> Machines { get; set; }
    }
}
