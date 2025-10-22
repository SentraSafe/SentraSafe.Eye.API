using EYEAPI.Models.Dtos.TelemtryDtos;
using EYEAPI.Services.TelemetryService;
using Microsoft.AspNetCore.Mvc;

namespace EYEAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
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
}