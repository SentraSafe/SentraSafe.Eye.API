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
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNewLocationAsync(CreateLocationDto newLocation)
        {
            return Ok(await locationService.AddLocationAsync(newLocation));
        }
    }
}
