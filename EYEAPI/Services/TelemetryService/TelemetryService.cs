using EYEAPI.Contexts;
using EYEAPI.Exstensions;
using EYEAPI.Models.Dtos.TelemtryDtos;
using EYEAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EYEAPI.Services.TelemetryService;

public class TelemetryService(WarehouseContext context) : ITelemetryService
{
    public async Task<List<Telemetry>> GetTelemetry(TelemetrySearchParams? searchParams)
    {
        IQueryable<Telemetry> query = context.Telemetry
            .WhereIfNotNull(searchParams?.MachineId, telemetry => telemetry.MachineId == searchParams.MachineId)
            .WhereIfNotNull(searchParams?.MeasurementType, telemetry => telemetry.MeasurementType == searchParams.MeasurementType)
            .WhereIfNotNull(searchParams?.Value, telemetry => telemetry.Value == searchParams.Value)
            .WhereIfNotNull(searchParams?.Device, telemetry => telemetry.Device == searchParams.Device)
            .WhereIfNotNull(searchParams?.ReadingTimeFrom, telemetry => telemetry.ReadingTime >= searchParams.ReadingTimeFrom)
            .WhereIfNotNull(searchParams?.ReadingTimeTo, telemetry => telemetry.ReadingTime <= searchParams.ReadingTimeTo);
        string squery = query.ToQueryString();
        return await query.ToListAsync();
    }

    public async Task<List<AnalyticsTelemetry>> GetAnalyticsTelemetry(AnalyticsTelemetrySearchParams? searchParams)
    {
        return await context.AnalyticsTelemetry
            .WhereIfNotNull(searchParams?.MachineId, telemetry => telemetry.MachineId == searchParams.MachineId)
            .WhereIfNotNull(searchParams?.MeasurementType, telemetry => telemetry.MeasurementType == searchParams.MeasurementType)
            .WhereIfNotNull(searchParams?.Value, telemetry => telemetry.Value == searchParams.Value)
            .WhereIfNotNull(searchParams?.Device, telemetry => telemetry.Device == searchParams.Device)
            .WhereIfNotNull(searchParams?.ReadingTimeFrom, telemetry => telemetry.ReadingTime >= searchParams.ReadingTimeFrom)
            .WhereIfNotNull(searchParams?.ReadingTimeTo, telemetry => telemetry.ReadingTime <= searchParams.ReadingTimeTo)
            .WhereIfNotNull(searchParams?.SublocationId, telemetry => telemetry.SublocationId == searchParams.SublocationId)
            .WhereIfNotNull(searchParams?.LocationId, telemetry => telemetry.LocationId == searchParams.LocationId)
            .ToListAsync();
    }
}