using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingAPI.Services;

namespace ParkingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidentController : ControllerBase
    {
        private readonly IResidentService _ResidentService;

        public ResidentController(IResidentService ResidentService)
        {
            _ResidentService = ResidentService;
        }

        // GET: api/Resident
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resident>>> GetResidents()
        {
            var Residents = await _ResidentService.GetAllResidents();
            return Ok(Residents);
        }

        // GET: api/Resident/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Resident>> GetResident(int id)
        {
            var Resident = await _ResidentService.GetResidentById(id);
            if (Resident == null)
            {
                return NotFound();
            }
            return Ok(Resident);
        }

        // POST: api/Resident
        [HttpPost]
        public async Task<ActionResult<Resident>> AddResident(Resident Resident)
        {
            if (Resident == null)
            {
                return BadRequest();
            }

            await _ResidentService.AddResident(Resident);
            return CreatedAtAction(nameof(GetResident), new { id = Resident.Id}, Resident);
        }

        // PUT: api/Resident/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResident(int id, Resident Resident)
        {
            if (id != Resident.Id)
            {
                return BadRequest();
            }

            try
            {
                await _ResidentService.UpdateResident(Resident);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _ResidentService.GetResidentById(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Resident/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResident(int id)
        {
            var Resident = await _ResidentService.GetResidentById(id);
            if (Resident == null)
            {
                return NotFound();
            }

            await _ResidentService.DeleteResident(id);
            return NoContent();
        }
    }
}
