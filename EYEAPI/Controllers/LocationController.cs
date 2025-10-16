using EYEAPI.Models.Dtos.AlarmDtos;
using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Services.LocationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EYEAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController(ISublocationService locationService) : ControllerBase
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
