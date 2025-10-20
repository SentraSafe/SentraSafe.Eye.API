using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.AlarmDtos
{
    public class AlarmSearchParamsDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public int? MachineId { get; set; }
        public int? ValueType { get; set; }
        public int? Value { get; set; }
        public Severity? Severity { get; set; }
    }
}
