using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.AlarmDtos
{
    public class CreateAlarmDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MachineId { get; set; }
        public int ValueType { get; set; }
        public Severity Severity { get; set; }
        public int MaximumValue { get; set; }
    }
}
