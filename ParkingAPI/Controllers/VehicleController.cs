using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingAPI.Services;

namespace ParkingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _VehicleService;

        public VehicleController(IVehicleService VehicleService)
        {
            _VehicleService = VehicleService;
        }

        // GET: api/Vehicle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            var Vehicles = await _VehicleService.GetAllVehicles();
            return Ok(Vehicles);
        }

        // GET: api/Vehicle/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var Vehicle = await _VehicleService.GetVehicleById(id);
            if (Vehicle == null)
            {
                return NotFound();
            }
            return Ok(Vehicle);
        }

        // POST: api/Vehicle
        [HttpPost]
        public async Task<ActionResult<Vehicle>> AddVehicle(Vehicle Vehicle)
        {
            if (Vehicle == null)
            {
                return BadRequest();
            }

            await _VehicleService.AddVehicle(Vehicle);
            return CreatedAtAction(nameof(GetVehicle), new { id = Vehicle.Id}, Vehicle);
        }

        // PUT: api/Vehicle/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, Vehicle Vehicle)
        {
            if (id != Vehicle.Id)
            {
                return BadRequest();
            }

            try
            {
                await _VehicleService.UpdateVehicle(Vehicle);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _VehicleService.GetVehicleById(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Vehicle/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var Vehicle = await _VehicleService.GetVehicleById(id);
            if (Vehicle == null)
            {
                return NotFound();
            }

            await _VehicleService.DeleteVehicle(id);
            return NoContent();
        }
    }
}
