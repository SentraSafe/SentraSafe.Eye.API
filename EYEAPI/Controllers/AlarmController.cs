using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Services.AlarmService;
using Microsoft.AspNetCore.Mvc;

namespace EYEAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlarmController(IAlarmService alarmService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAlarmsAsync([FromQuery] AlarmSearchParamsDto searchParams)
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

        [HttpPost]
        public async Task<IActionResult> PostNewAlarmAsync(CreateAlarmDto newAlarm)
        {
            try
            {
                return Ok(await alarmService.AddAlarmAsync(newAlarm));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutUpdateAlarmAsync(Alarm alarm)
        {
            try
            {
                return Ok(await alarmService.UpdateAlarmAsync(alarm));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAlarmByIdAsync(int machinceId)
        {
            try
            {
                await alarmService.DeleteAlarmByIdAsync(machinceId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
