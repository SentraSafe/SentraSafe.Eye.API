using EYEAPI.Models.Enums;

namespace EYEAPI.Models.Dtos.LogDtos
{
    public class LogSearchParamsDto
    {
        TimeSpan? Time { get; set; }
        Severity? Severity { get; set; }
        int? MachineID { get; set; }
        int? AlarmId { get; set; }
        bool? IsHandled { get; set; }
        string? HandledBy { get; set; }
    }
}
