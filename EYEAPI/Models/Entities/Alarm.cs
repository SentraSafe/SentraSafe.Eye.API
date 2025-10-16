using EYEAPI.Models.Enums;
using Serverity = EYEAPI.Models.Enums.Serverity;

namespace EYEAPI.Models.Entities
{
    public class Alarm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; }
        public MachineType MachineType { get; set; }
        public Serverity Severity { get; set; }
        public int MaximumValue { get; set; }
    }
}
