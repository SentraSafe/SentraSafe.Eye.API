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
    }
}
