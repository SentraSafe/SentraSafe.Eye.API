using EYEAPI.Models.Dtos.TelemtryDtos;
using EYEAPI.Services.TelemetryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EYEAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize("Analytics")]
public class TelemetryController(ITelemetryService telemetryService) : ControllerBase
{
    [HttpGet("GetTelemetry")]
    public async Task<IActionResult> GetTelemetry([FromQuery] TelemetrySearchParams? searchParams)
    {
        return Ok(await telemetryService.GetTelemetry(searchParams));
    }
    
    [HttpGet("GetAnalyticsTelemetry")]
    public async Task<IActionResult> GetAnalyticsTelemetry([FromQuery] AnalyticsTelemetrySearchParams? searchParams)
    {
        return Ok(await telemetryService.GetAnalyticsTelemetry(searchParams));
    }
    
    [HttpGet("GetLatestDistinctMeasurementById")]
    public async Task<IActionResult> GetLatestDistinctMeasurementByIdAsync(int id)
    {
        return Ok(await telemetryService.GetLatestDistinctMeasurementByIdAsync(id));
    }
}