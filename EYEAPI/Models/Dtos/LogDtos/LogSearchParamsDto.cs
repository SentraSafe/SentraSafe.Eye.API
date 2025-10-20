using AutoMapper;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.LogDtos
{
    public class LogSearchParamsDto
    {
        public DateTime? TimeStampFrom { get; set; }
        public DateTime? TimeStampTo { get; set; }
        public Severity? Severity { get; set; }
        public int? MachineID { get; set; }
        public int? AlarmId { get; set; }
        public bool? IsHandled { get; set; }
        public string? HandledBy { get; set; }
        public DateTime? HandleTimeFrom { get; set; }
        public DateTime? HandleTimeTo { get; set; }
    }
}
