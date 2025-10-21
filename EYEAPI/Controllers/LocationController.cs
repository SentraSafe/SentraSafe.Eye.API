using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Services.LocationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EYEAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationController(ILocationService locationService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllLocationsAsync()
        {
            try
            {
                return Ok(await locationService.GetAllLocationsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostNewLocationAsync(CreateLocationDto newLocation)
        {
            try
            {
                return Ok(await locationService.AddLocationAsync(newLocation));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateLocationAsync(LocationDto location)
        {
            try
            {
                return Ok(await locationService.UpdateLocationAsync(location));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteLocationByIdAsync(int locationId)
        {
            try
            {
                await locationService.DeleteLocationByIdAsync(locationId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
