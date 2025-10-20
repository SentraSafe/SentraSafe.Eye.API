using AutoMapper;
using EYEAPI.Models.Dtos.SublocationDtos;
using EYEAPI.Models.Entities;

namespace EYEAPI.Models.Dtos.LocationDtos
{
    [AutoMap(typeof(Location))]
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? MachineCount { get; set; }
        public List<SublocationDto>? Sublocations { get; set; }
    }
}
