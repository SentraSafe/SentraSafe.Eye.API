using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;
using EYEAPI.Repositories;
using EYEAPI.Services.MachineService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
using SharpCompress.Archives;

namespace EYEAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController(IMachineService machineService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMachinesAsync([FromQuery] MachineSearchParamsDto searchParams)
        {
            try
            {
                return Ok(await machineService.GetMachinesAsync(searchParams));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> PostNewMachineAsync(CreateMachineDto newMachine)
        {
            try
            {
                return Ok(await machineService.AddMachineAsync(newMachine));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutUpdateMachineAsync([FromQuery] Machine machine)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
