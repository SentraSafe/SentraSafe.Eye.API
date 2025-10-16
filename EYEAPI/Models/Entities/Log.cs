using EYEAPI.Models.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EYEAPI.Models.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public Severity Severity { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; }
        public int AlarmId { get; set; }
        public Alarm Alarm { get; set; }
    }
}
