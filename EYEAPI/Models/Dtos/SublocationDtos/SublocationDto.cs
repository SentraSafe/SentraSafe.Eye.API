using EYEAPI.Models.Entities;
using AutoMapper;

namespace EYEAPI.Models.Dtos.SublocationDtos
{
    [AutoMap(typeof(Sublocation))]
    public class SublocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
    }
}
