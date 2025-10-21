using EYEAPI.Models.Dtos.LogDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Services.LogService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EYEAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController(ILogService logService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetLogsAsync([FromQuery]EventLogSearchParamsDto searchParams)
        {
            try
            {
                return Ok(await logService.GetEventLogsAsync(searchParams));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNewEventLogAsync(CreateEventLogDto newEventLog)
        {
            try
            {
                return Ok(await logService.AddEventLogAsync(newEventLog));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("HandleLog")]
        public async Task<IActionResult> HandleLogAsync(HandleEventLogDto newEventLog)
        {
            try
            {
                await logService.HandleEventLog(newEventLog);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Bulk")]
        public async Task<IActionResult> AddEventLogsAsync(List<EventLogDto> newEventLogs)
        {
            try
            {
                await logService.AddEventLogsAsync(newEventLogs);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
