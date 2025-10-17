using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.LogDtos
{
    public class LogDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; } 
        public Severity Severity { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; }
        public int AlarmId { get; set; }
        public Alarm Alarm { get; set; }
        public string Value { get; set; }
        public bool IsHandled { get; set; }
        public string HandledBy { get; set; }
        public DateTime HandleTime { get; set; }
        public LogDto(Log log)
        {
            Id = log.Id;
            Description = log.Description;
            TimeStamp = log.TimeStamp;
            Severity = log.Severity;
            MachineId = log.MachineId;
            AlarmId = log.AlarmId;
            Alarm = log.Alarm;
            Value = log.Value;
            IsHandled = log.IsHandled;
            HandledBy = log.HandledBy;
            HandleTime = log.HandleTime;

        }
    }
}
