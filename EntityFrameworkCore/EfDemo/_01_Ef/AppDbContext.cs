using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Ef
{
    public class AppDbContext : DbContext
    {
        private string connectionString= "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=EF._01_FullDefined;";

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(builder => {
                builder.ToTable(nameof(Activity));
                builder.HasKey(m => m.Id);
                builder.Property(m => m.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<WxGroup>(builder => {
                builder.ToTable(nameof(WxGroup));
                builder.HasKey(m => m.Id);
                builder.Property(m => m.Id).ValueGeneratedOnAdd();

                builder.HasOne<Activity>().WithMany(m => m.WxGroups).HasForeignKey(m => m.ActivityId);
            });
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    public class Activity : StringIdEntity
    {
        public string Name { get; set; }
        public string Introduction { get; set; }
        public string QRCodePath { get; set; }

        public List<WxGroup> WxGroups { get; set; }
    }

    public class WxGroup : StringIdEntity
    {
        public string Name { get; set; }
        public int VisitedCount { get; set; }
        public string QRCodePath { get; set; }

        public string ActivityId { get; set; }
    }

    public class StringIdEntity
    {
        public string Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
