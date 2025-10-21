using AutoMapper;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.LogDtos
{
    public class EventLogSearchParamsDto
    {
        public DateTime? TimeStampFrom { get; set; }
        public DateTime? TimeStampTo { get; set; }
        public Severity? Severity { get; set; }
        public int[]? MachineIds { get; set; }
        public int? AlarmId { get; set; }
        public bool? IsHandled { get; set; }
        public string? HandledBy { get; set; }
        public DateTime? HandledFrom { get; set; }
        public DateTime? HandledTo { get; set; }
        public bool? AlarmIdNotNull { get; set; }
    }
}
