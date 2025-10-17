using System.Text.Json.Serialization;

namespace EYEAPI.Models.Dtos.LogDtos
{
    public class EventLogDto
    {
        public int MachineId { get; set; }
        public int Severity { get; set; }
        public DateTime TimeCreated { get; set; }
        public string Source { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
