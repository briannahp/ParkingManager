namespace ParkingAPI.Services
{
    public interface IResidentService
    {
        Task<IEnumerable<Resident>> GetAllResidents();
        Task<Resident> GetResidentById(int id);
        Task AddResident(Resident Resident);
        Task UpdateResident(Resident Resident);
        Task DeleteResident(int id);
    }
}