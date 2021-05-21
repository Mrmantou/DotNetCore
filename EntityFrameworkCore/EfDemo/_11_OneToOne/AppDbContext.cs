using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_OneToOne

{
    public class AppDbContext : DbContext
    {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=EF._11_OneToOne;";

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().HasOne(b => b.BlogImage).WithOne(i => i.Blog).HasForeignKey<BlogImage>(b => b.BlogForeignKey);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public BlogImage BlogImage { get; set; }
    }

    public class BlogImage
    {
        public int BlogImageId { get; set; }
        public byte[] Image { get; set; }
        public string Caption { get; set; }

        public int BlogForeignKey { get; set; }
        public Blog Blog { get; set; }
    }
}
