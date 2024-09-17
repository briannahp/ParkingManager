namespace ParkingAPI.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAllVehicles();
        Task<Vehicle> GetVehicleById(int id);
        Task AddVehicle(Vehicle Vehicle);
        Task UpdateVehicle(Vehicle Vehicle);
        Task DeleteVehicle(int id);
    }
}