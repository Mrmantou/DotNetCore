using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_ShadowForeignKey

{
    public class AppDbContext : DbContext
    {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=EF._08_ShadowForeignKey;";

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add the shadow property to the model
            modelBuilder.Entity<Post>().Property<int>("BlogForeignKey");

            // Use the shadow property as a foreign key
            modelBuilder.Entity<Post>().HasOne(p => p.Blog).WithMany(b => b.Posts).HasForeignKey("BlogForeignKey");
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

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Blog Blog { get; set; }
    }
}
