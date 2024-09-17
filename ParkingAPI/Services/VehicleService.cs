using Microsoft.EntityFrameworkCore;
using ParkingAPI.AppData;

namespace ParkingAPI.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly AccountContext _context;

        public VehicleService(AccountContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehicles()
        {
            return await _context.Vehicles
                .ToListAsync();
        }

        public async Task<Vehicle> GetVehicleById(int id)
        {
            return await _context.Vehicles.FindAsync(id);

        }

        public async Task AddVehicle(Vehicle Vehicle)
        {
            _context.Vehicles.Add(Vehicle);
             await _context.SaveChangesAsync();
        }

        public async Task UpdateVehicle(Vehicle Vehicle)
        {
            _context.Vehicles.Update(Vehicle);
            await _context.SaveChangesAsync();        
        }

        public async Task DeleteVehicle(int id)
        {
           var Vehicle = await _context.Vehicles.FindAsync(id);
            if (Vehicle != null)
            {
                _context.Vehicles.Remove(Vehicle);
                await _context.SaveChangesAsync();
            }
        }
    }
}
