using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Services.AlarmService;
using EYEAPI.Services.MachineService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EYEAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlarmController(IAlarmService alarmService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMachinesAsync([FromQuery] AlarmSearchParamsDto searchParams)
        {
            try
            {
                return Ok(await alarmService.GetAlarmsAsync(searchParams));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
