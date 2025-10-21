using EYEAPI.Models.Dtos.SublocationDtos;
using EYEAPI.Services.SublocationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EYEAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SublocationController(ISublocationService sublocationService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSublocationsAsync(int sublocationId)
        {
            try
            {
                return Ok(await sublocationService.GetSublocationByLocationAsync(sublocationId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostNewSublocationAsync(CreateSublocationDto newSublocation)
        {
            try
            {
                return Ok(await sublocationService.AddSublocationAsync(newSublocation));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("Bulk")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostNewSublocationsAsync(List<CreateSublocationDto> newSublocations)
        {
            try
            {
                await sublocationService.AddSublocationsAsync(newSublocations);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSublocationAsync(SublocationDto location)
        {
            try
            {
                return Ok(await sublocationService.UpdateSublocationAsync(location));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSublocationByIdAsync(int locationId)
        {
            try
            {
                await sublocationService.DeleteSublocationByIdAsync(locationId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
