using System;
using Microsoft.EntityFrameworkCore;
using ParkingAPI.AppData;

namespace ParkingAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountContext _context;

        public AccountService(AccountContext context)
        {
            _context = context;
        }

        // Get all parking accounts
        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _context.Accounts
                .Include(pa => pa.Residents)
                .Include(pa => pa.Vehicles)
                .ToListAsync();
        }

        // Get a parking account by ID
        public async Task<Account> GetAccountById(int id)
        {
            try {
            return await _context.Accounts
                .Include(acc => acc.Residents)
                .Include(acc => acc.Vehicles)
                .FirstOrDefaultAsync(acc => acc.Id == id);
            }
            catch(NullReferenceException)
            {
                Console.WriteLine("Unable to retrieve account for ID: %d", id);
                return new Account();
            }
        }

        // Create a new parking account
        public async Task CreateAccount(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        // Update a parking account
        public async Task UpdateAccount(Account account)
        {
            var acc = _context.Accounts.Where(a => a.Id == account.Id).AsNoTracking().FirstOrDefault();
            if (acc!=null){
                account.Id = acc.Id; // update primary key
            }
            else
            {
                account.Id = 0; //insert new entity.
            }
            _context.Entry(account).State = !_context.Accounts.Any(a => a.Id == account.Id) ? EntityState.Added : EntityState.Modified;
             await _context.SaveChangesAsync();
        }

        // Delete a parking account
        public async Task DeleteAccount(int id)
        {
             var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

    }
}
