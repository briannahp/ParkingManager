using Microsoft.EntityFrameworkCore;

namespace ParkingAPI.AppData
{
    public class AccountContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Resident> Residents { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ParkingSpace> ParkingSpaces { get; set; }

        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
                 Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Seed data for SQLite DB
            List<Account> seedAccounts = [
                new Account { Id = 12, FamilyName = "Peterson Family", Email = "hopeannabri@gmail.com", Phone = "111-222-3333" }, 
                new Account { Id = 14, FamilyName = "Hanson Family", Email = "FakeEmail@gmail.com", Phone = "333-444-5555" }, 
                new Account { Id = 15, FamilyName = "Tran Family", Email = "FakeEmail2@gmail.com", Phone = "777-888-9999" }];
            List<Resident> seedResidents = [
                new Resident { Id = 8, AccountId = seedAccounts[0].Id, FirstName = "Brianna", LastName = "Peterson" },
                new Resident { Id = 2, AccountId = seedAccounts[0].Id, FirstName = "Jace", LastName = "Peterson", AltPhoneNumber = "222-444-9999" },
                new Resident { Id = 3, AccountId = seedAccounts[1].Id, FirstName = "Anna", LastName = "Hanson" },
                new Resident { Id = 4, AccountId = seedAccounts[2].Id, FirstName = "Sydney", LastName = "Tran" },
                new Resident { Id = 5, AccountId = seedAccounts[2].Id, FirstName = "Emmanuel", LastName = "Rocha", AltPhoneNumber = "555-555-9999" }
            ];
            List<Vehicle> seedVehicles = [               
                new Vehicle { Id = 1, Make = "Toyota", Color = "Black", Model = "Corolla", AccountId = seedAccounts[0].Id,  SpotId = 111 },
                new Vehicle { Id = 2, Make = "Honda", Color = "Blue", Model = "Civic", AccountId = seedAccounts[1].Id,  SpotId = 201 },
                new Vehicle { Id = 3, Make = "Honda", Color = "Red", Model = "Accord", AccountId = seedAccounts[2].Id,  SpotId = 101 },
                new Vehicle { Id = 4, Make = "Subaru", Color = "Silver", Model = "Crosstrek", AccountId = seedAccounts[2].Id, SpotId = 113 }
            ];
            List<ParkingSpace> seedParkingSpaces = [
                new ParkingSpace { Id = 201, IsTaken = true, VehicleId = seedVehicles[1].Id},
                new ParkingSpace { Id = 202, IsTaken = false},
                new ParkingSpace { Id = 101, IsTaken = true, VehicleId = seedVehicles[2].Id },
                new ParkingSpace { Id = 111, IsTaken = true, VehicleId = seedVehicles[0].Id },
                new ParkingSpace { Id = 113, IsTaken = true, VehicleId = seedVehicles[3].Id}
            ];

            // Specify table names
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<Resident>().ToTable("Residents");
            modelBuilder.Entity<Vehicle>().ToTable("Vehicles");
            modelBuilder.Entity<ParkingSpace>().ToTable("ParkingSpaces");


            modelBuilder.Entity<Account>()
                .HasMany(a => a.Residents)
                .WithOne(r => r.Account)
                .HasForeignKey(r => r.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Vehicles)
                .WithOne(v => v.Account)
                .HasForeignKey(v => v.AccountId)
                .OnDelete(DeleteBehavior.Cascade);      

            modelBuilder.Entity<ParkingSpace>()
                 .HasOne(p => p.Vehicle)
                 .WithOne(v => v.ParkingSpace)
                 .HasForeignKey<Vehicle>(v => v.SpotId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.SetNull);

        
            modelBuilder.Entity<Vehicle>()
                 .HasOne(v => v.ParkingSpace)
                 .WithOne(p => p.Vehicle)
                 .HasForeignKey<ParkingSpace>(p => p.VehicleId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.SetNull);   

            modelBuilder.Entity<Account>().HasData(seedAccounts);
            modelBuilder.Entity<Resident>().HasData(seedResidents);
            modelBuilder.Entity<Vehicle>().HasData(seedVehicles);
            modelBuilder.Entity<ParkingSpace>().HasData(seedParkingSpaces);
          
        }
    }
}
