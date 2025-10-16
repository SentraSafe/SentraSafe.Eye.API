using AutoMapper;
using EYEAPI.Models.Dtos.LocationDtos;

namespace EYEAPI.Models.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Sublocation>? Sublocations { get; set; } = new List<Sublocation>();
    }
}
