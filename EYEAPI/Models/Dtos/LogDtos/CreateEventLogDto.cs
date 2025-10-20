using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.LogDtos
{
    public class CreateEventLogDto
    {
        public int MachineId { get; set; }
        public Severity Severity { get; set; }
        public DateTime TimeCreated { get; set; }
        public string Source { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int? AlarmId { get; set; }
    }
}
