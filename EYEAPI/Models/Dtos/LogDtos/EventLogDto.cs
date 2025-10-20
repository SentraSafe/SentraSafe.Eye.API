using System.Text.Json.Serialization;
using AutoMapper;
using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.LogDtos
{
    [AutoMap(typeof(EventLog))]
    public class EventLogDto
    {
        public int MachineId { get; set; }
        public MachineDto Machine { get; set; }
        public Severity Severity { get; set; }
        public DateTime TimeCreated { get; set; }
        public string Source { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int? AlarmId { get; set; }
        public AlarmDto? Alarm { get; set; }
        public string? HandledBy { get; set; }
        public DateTime? HandledAt { get; set; }
        public string? HandledFeedback { get; set; }
    }
}
