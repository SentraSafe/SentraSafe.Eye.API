using Microsoft.EntityFrameworkCore;

namespace EYEAPI.Models.Entities;

[Keyless]
public class AnalyticsTelemetry
{
    public string Device { get; set; }
    public int MachineId { get; set; }
    public int LocationId { get; set; }
    public int SublocationId { get; set; }
    public int MeasurementType { get; set; }
    public int Value { get; set; }
    public DateTime ReadingTime { get; set; }
}