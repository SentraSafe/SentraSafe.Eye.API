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
        public async Task<IActionResult> GetLogsAsync([FromQuery]LogSearchParamsDto searchParams)
        {
            try
            {
                return Ok(await logService.GetLogsAsync(searchParams));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNewLogAsync(CreateLogDto newLog)
        {
            try
            {
                return Ok(await logService.AddLogAsync(newLog));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("HandleLog")]
        public async Task<IActionResult> HandleLogAsync(HandleLogDto newLog)
        {
            try
            {
                await logService.HandleLog(newLog);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLogAsync(Log log)
        {
            try
            {
                return Ok(await logService.UpdateLogAsync(log));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLogByIdAsync(int locationId)
        {
            try
            {
                await logService.DeleteLogByIdAsync(locationId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
