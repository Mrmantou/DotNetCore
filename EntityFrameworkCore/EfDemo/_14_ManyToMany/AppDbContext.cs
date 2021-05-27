using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14_ManyToMany
{
    public class AppDbContext : DbContext
    {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=EF._14_ManyToMany;";

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .UsingEntity<PostTagBinding>(
                    j => j.HasOne<Tag>().WithMany().HasForeignKey(m => m.TagId), 
                    j => j.HasOne<Post>().WithMany().HasForeignKey(m => m.PostId));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }

    public class Tag
    {
        public string TagId { get; set; } = Guid.NewGuid().ToString();
        public string TagName { get; set; }

        public ICollection<Post> Posts { get; set; }
    }

    public class PostTagBinding
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string TagId { get; set; }
    }
}
