namespace EYEAPI.Models.Dtos.TelemtryDtos;

public class AnalyticsTelemetrySearchParams : TelemetrySearchParams
{
    public int? LocationId { get; set; }
    public int? SublocationId { get; set; }
}