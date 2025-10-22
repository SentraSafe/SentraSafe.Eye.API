using EYEAPI.Models.Dtos.TelemtryDtos;
using EYEAPI.Models.Entities;

namespace EYEAPI.Services.TelemetryService;

public interface ITelemetryService
{
    Task<List<Telemetry>> GetTelemetry(TelemetrySearchParams? searchParams);
    Task<List<AnalyticsTelemetry>> GetAnalyticsTelemetry(AnalyticsTelemetrySearchParams? searchParams);
    Task<List<AnalyticsTelemetry>> GetLatestDistinctMeasurementByIdAsync(int machineId);
}