using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconMiniProject03
{
    public class MyDbContext : DbContext
    {
        string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=EFassetTracker;Trusted_Connection=True;Encrypt=False;";

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Office> Offices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Base table for Asset
            modelBuilder.Entity<Asset>().ToTable("Assets");

            // Derived tables for TPT
            modelBuilder.Entity<Computer>().ToTable("Computers");
            modelBuilder.Entity<Phone>().ToTable("Phones");

            modelBuilder.Entity<Asset>()
                .HasOne((a => a.OfficeLocation))
                .WithMany(o => o.Assets)
                .HasForeignKey(o => o.OfficeId);

            modelBuilder.Entity<Office>().HasData(
                new Office { Id = 1, Name = "New York", CurrencyCode = "USD" },
                new Office { Id = 2, Name = "London", CurrencyCode = "GBP" },
                new Office { Id = 3, Name = "Berlin", CurrencyCode = "EUR" },
                new Office { Id = 4, Name = "San Diego", CurrencyCode = "USD" }
             );
            modelBuilder.Entity<Computer>().HasData(
                new Computer { Id = 1, Model = "Dell", PurchaseDate = new DateTime(2019, 6, 10), PriceUSD = 1500, OfficeId = 1 },
                new Computer { Id = 2, Model = "Samsung", PurchaseDate = new DateTime(2021, 1, 25), PriceUSD = 800, OfficeId = 2 },
                new Computer { Id = 3, Model = "Samsung", PurchaseDate = new DateTime(2021, 1, 15), PriceUSD = 1500, OfficeId = 2 },
                new Computer { Id = 4, Model = "Dell", PurchaseDate = new DateTime(2019, 6, 10), PriceUSD = 1500, OfficeId = 1 },
                new Computer { Id = 5, Model = "MacBook", PurchaseDate = new DateTime(2021, 1, 20), PriceUSD = 1500, OfficeId = 3 },
                new Computer { Id = 6, Model = "MacBook", PurchaseDate = new DateTime(2021, 1, 20), PriceUSD = 1500, OfficeId = 3 }

            );
            modelBuilder.Entity<Phone>().HasData(new Phone { Id = 7, Model = "Iphone", PurchaseDate = new DateTime(2022, 1, 5), PriceUSD = 1500, OfficeId = 1 },
                new Phone { Id = 8, Model = "Iphone", PurchaseDate = new DateTime(2022, 1, 5), PriceUSD = 1500, OfficeId = 1 },
                new Phone { Id = 9, Model = "Huawei", PurchaseDate = new DateTime(2023, 2, 20), PriceUSD = 1500, OfficeId = 2 },
                new Phone { Id = 10, Model = "Huawei", PurchaseDate = new DateTime(2023, 2, 20), PriceUSD = 1500, OfficeId = 2 },
                new Phone { Id = 11, Model = "Nokia", PurchaseDate = new DateTime(2022, 1, 1), PriceUSD = 1500, OfficeId = 3 },
                new Phone { Id = 12, Model = "Nokia", PurchaseDate = new DateTime(2022, 1, 1), PriceUSD = 1500, OfficeId = 3 }
                );

            base.OnModelCreating(modelBuilder);
        }

    }
}
