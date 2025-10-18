using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.LogDtos
{
    public class CreateLogDto
    {
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public Severity Severity { get; set; }
        public int MachineId { get; set; }
        public int AlarmId { get; set; }
    }
}
