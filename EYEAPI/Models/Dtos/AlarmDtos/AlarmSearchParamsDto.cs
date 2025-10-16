using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.AlarmDtos
{
    public class AlarmSearchParamsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MachineId { get; set; }
        public MachineType MachineType { get; set; }
        public Serverity Severity { get; set; }
    }
}
