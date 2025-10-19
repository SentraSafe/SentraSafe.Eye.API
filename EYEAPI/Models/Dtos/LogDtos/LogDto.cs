using AutoMapper;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.LogDtos
{
    [AutoMap(typeof(Log))]
    public class LogDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime TimeStamp { get; set; }
        public Severity Severity { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; }
        public int? AlarmId { get; set; }
        public Alarm? Alarm { get; set; }
        public string Value { get; set; }
        public bool IsHandled { get; set; }
        public string? HandledBy { get; set; }
        public DateTime HandleTime { get; set; }
        public string? HandleDescription { get; set; }
    }
}
