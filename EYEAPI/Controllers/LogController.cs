using EYEAPI.Models.Dtos.SublocationDtos;
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
        public async Task<IActionResult> GetSublocationsAsync(int sublocationId)
        {
            try
            {
                return Ok(await logService.GetSublocationByLocationAsync(sublocationId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNewSublocationAsync(CreateSublocationDto newSublocation)
        {
            try
            {
                return Ok(await logService.AddSublocationAsync(newSublocation));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSublocationAsync(SublocationDto location)
        {
            try
            {
                return Ok(await logService.UpdateSublocationAsync(location));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSublocationByIdAsync(int locationId)
        {
            try
            {
                await logService.DeleteSublocationByIdAsync(locationId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
