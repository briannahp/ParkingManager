using Microsoft.EntityFrameworkCore;
using ParkingAPI.AppData;

namespace ParkingAPI.Services
{
    public class ResidentService : IResidentService
    {
        private readonly AccountContext _context;

        public ResidentService(AccountContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Resident>> GetAllResidents()
        {
            return await _context.Residents
                .ToListAsync();
        }

        public async Task<Resident> GetResidentById(int id)
        {
            return await _context.Residents.FindAsync(id);

        }

        public async Task AddResident(Resident Resident)
        {
            _context.Residents.Add(Resident);
             await _context.SaveChangesAsync();
        }

        public async Task UpdateResident(Resident Resident)
        {
            _context.Residents.Update(Resident);
            await _context.SaveChangesAsync();        
        }

        public async Task DeleteResident(int id)
        {
           var Resident = await _context.Residents.FindAsync(id);
            if (Resident != null)
            {
                _context.Residents.Remove(Resident);
                await _context.SaveChangesAsync();
            }
        }
    }
}
