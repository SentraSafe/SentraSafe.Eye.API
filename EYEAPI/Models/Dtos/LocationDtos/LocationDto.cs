using EYEAPI.Models.Entities;

namespace EYEAPI.Models.Dtos.LocationDtos
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Sublocation> Sublocations{ get; set; }

        public LocationDto(Location location)
        {
            Id = location.Id;
            Name = location.Name;
            Sublocations = location.Sublocations.ToList();
        }
    }
}
