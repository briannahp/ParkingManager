namespace ParkingAPI.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccounts();
        
        Task<Account> GetAccountById(int id);
        
        Task CreateAccount(Account account);
        
        Task UpdateAccount(Account account);
        
        Task DeleteAccount(int id);
        
    }
}