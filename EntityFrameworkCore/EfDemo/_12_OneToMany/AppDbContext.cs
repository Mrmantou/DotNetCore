using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_OneToMany

{
    public class AppDbContext : DbContext
    {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=EF._12_OneToMany;";

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class).WithMany().HasForeignKey("FK_ClassId");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public Class Class { get; set; }
    }

    public class Class
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
