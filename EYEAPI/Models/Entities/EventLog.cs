using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Entities
{
    public class EventLog
    {
        public int MachineId { get; set; }
        public Machine Machine { get; set; }
        public Severity Severity { get; set; }
        public DateTime TimeCreated { get; set; }
        public string Source { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int? AlarmId { get; set; }
        public Alarm? Alarm { get; set; }
        public string? HandledBy { get; set; }
        public DateTime? HandledAt { get; set; }
        public string? HandledFeedback { get; set; }
    }
}
