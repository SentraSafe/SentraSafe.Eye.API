using AutoMapper;
using EYEAPI.Models.Dtos.SublocationDtos;

namespace EYEAPI.Models.Entities
{
    [AutoMap(typeof(CreateSublocationDto))]
    public class Sublocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public ICollection<Machine> Machines { get; set; }
    }
}
