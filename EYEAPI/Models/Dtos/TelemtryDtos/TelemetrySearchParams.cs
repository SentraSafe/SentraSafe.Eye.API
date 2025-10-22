namespace EYEAPI.Models.Dtos.TelemtryDtos;

public class TelemetrySearchParams
{
    public string? Device { get; set; }
    public int? MachineId { get; set; }
    public int? MeasurementType { get; set; }
    public int? Value { get; set; }
    public DateTime? ReadingTimeFrom { get; set; }
    public DateTime? ReadingTimeTo { get; set; }
}