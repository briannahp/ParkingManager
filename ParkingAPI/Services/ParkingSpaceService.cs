using Microsoft.EntityFrameworkCore;
using ParkingAPI.AppData;

namespace ParkingAPI.Services
{
    public class ParkingSpaceService : IParkingSpaceService
    {
        private readonly AccountContext _context;

        public ParkingSpaceService(AccountContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParkingSpace>> GetAllSpaces()
        {
            return await _context.ParkingSpaces
                .Include(ps => ps.Vehicle)
                .ToListAsync();
        }

        public async Task<ParkingSpace> GetSpaceById(int id)
        {
            return await _context.ParkingSpaces.FindAsync(id);

        }

        public async Task AddParkingSpace(ParkingSpace parkingSpace)
        {
            _context.ParkingSpaces.Add(parkingSpace);
             await _context.SaveChangesAsync();
        }

        public async Task UpdateParkingSpace(ParkingSpace parkingSpace)
        {
            _context.ParkingSpaces.Update(parkingSpace);

            await _context.SaveChangesAsync();        
        }

        public async Task DeleteParkingSpace(int id)
        {
           var parkingSpace = await _context.ParkingSpaces.FindAsync(id);
            if (parkingSpace != null)
            {
                _context.ParkingSpaces.Remove(parkingSpace);
                await _context.SaveChangesAsync();
            }
        }
    }
}
