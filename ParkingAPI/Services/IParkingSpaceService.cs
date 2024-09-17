namespace ParkingAPI.Services
{
    public interface IParkingSpaceService
    {
        Task<IEnumerable<ParkingSpace>> GetAllSpaces();
        Task<ParkingSpace> GetSpaceById(int id);
        Task AddParkingSpace(ParkingSpace parkingSpace);
        Task UpdateParkingSpace(ParkingSpace parkingSpace);
        Task DeleteParkingSpace(int id);
    }
}