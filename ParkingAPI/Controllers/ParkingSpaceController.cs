using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingAPI.Services;


namespace ParkingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingSpaceController : ControllerBase
    {
        private readonly IParkingSpaceService _parkingSpaceService;

        public ParkingSpaceController(IParkingSpaceService parkingSpaceService)
        {
            _parkingSpaceService = parkingSpaceService;
        }

        // GET: api/ParkingSpace
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSpace>>> GetParkingSpaces()
        {
            var parkingSpaces = await _parkingSpaceService.GetAllSpaces();
            return Ok(parkingSpaces);
        }

        // GET: api/ParkingSpace/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSpace>> GetParkingSpace(int id)
        {
            var parkingSpace = await _parkingSpaceService.GetSpaceById(id);
            if (parkingSpace == null)
            {
                return NotFound();
            }
            return Ok(parkingSpace);
        }

        // POST: api/ParkingSpace
        [HttpPost]
        public async Task<ActionResult<ParkingSpace>> AddParkingSpace(ParkingSpace parkingSpace)
        {
            if (parkingSpace == null)
            {
                return BadRequest();
            }

            await _parkingSpaceService.AddParkingSpace(parkingSpace);
            return CreatedAtAction(nameof(GetParkingSpace), new { id = parkingSpace.Id}, parkingSpace);
        }

        // PUT: api/ParkingSpace/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParkingSpace(int id, ParkingSpace parkingSpace)
        {
            if (id != parkingSpace.Id)
            {
                return BadRequest();
            }

            try
            {
                await _parkingSpaceService.UpdateParkingSpace(parkingSpace);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _parkingSpaceService.GetSpaceById(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/ParkingSpace/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingSpace(int id)
        {
            var parkingSpace = await _parkingSpaceService.GetSpaceById(id);
            if (parkingSpace == null)
            {
                return NotFound();
            }

            await _parkingSpaceService.DeleteParkingSpace(id);
            return NoContent();
        }
    }
}
