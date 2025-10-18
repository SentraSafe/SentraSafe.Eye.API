using AutoMapper;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.LogDtos
{
    [AutoMap(typeof(Log))]
    public class LogSearchParamsDto
    {
        public TimeSpan? Time { get; set; }
        public Severity? Severity { get; set; }
        public int? MachineID { get; set; }
        public int? AlarmId { get; set; }
        public bool? IsHandled { get; set; }
        public string? HandledBy { get; set; }
    }
}
