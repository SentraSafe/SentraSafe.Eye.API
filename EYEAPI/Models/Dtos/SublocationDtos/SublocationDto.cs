using EYEAPI.Models.Entities;

namespace EYEAPI.Models.Dtos.SublocationDtos
{
    public class SublocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }

        public SublocationDto(Sublocation sublocation)
        {
            Id = sublocation.Id;
            Name = sublocation.Name;
            LocationId = sublocation.LocationId;

        }
    }
}
