using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10_PrincipalKey

{
    public class AppDbContext : DbContext
    {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=EF._10_PrincipalKey;";

        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecordOfSale>()
                .HasOne(s => s.Car)
                .WithMany(c => c.SaleHistory)
                .HasForeignKey(s => s.CarLicensePlate)
                .HasPrincipalKey(c => c.LicensePlate);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public class Car
    {
        public int CarId { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public List<RecordOfSale> SaleHistory { get; set; }
    }

    public class RecordOfSale
    {
        public int RecordOfSaleId { get; set; }
        public DateTime DateSold { get; set; }
        public decimal Price { get; set; }

        public string CarLicensePlate { get; set; }
        public Car Car { get; set; }
    }
}
