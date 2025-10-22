using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Entities;
using EYEAPI.Models.Enums;
using EYEAPI.Repositories;
using EYEAPI.Services.LocationService;
using EYEAPI.Services.MachineService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
using SharpCompress.Archives;

namespace EYEAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMachineAsync(int id)
        {
            try
            {
                return Ok(await machineService.GetMachineByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> PostNewMachineAsync([FromBody] CreateMachineDto newMachine)
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
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> PutUpdateMachineAsync(UpdateMachineDto machine)
        {
            try
            {
                return Ok(await machineService.UpdateMachineAsync(machine));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> DeleteMachineByIdAsync(int machinceId)
        {
            try
            {
                await machineService.DeleteMachineByIdAsync(machinceId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
