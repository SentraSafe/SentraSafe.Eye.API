using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using EYEAPI.Models.Dtos.LocationDtos;

namespace EYEAPI.Models.Entities
{
    [AutoMap(typeof(CreateLocationDto))]
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public int? MachineCount { get; set; }
        public ICollection<Sublocation> Sublocations { get; set; }
    }
}
