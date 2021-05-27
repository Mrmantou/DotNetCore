using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _15_ManyToMany
{
    public class AppDbContext : DbContext
    {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=EF._15_ManyToMany;";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public class OutPostMapping : IEntityTypeConfiguration<OutPost>
    {
        public void Configure(EntityTypeBuilder<OutPost> builder)
        {
            builder.ToTable("OutPost");
            builder.HasKey(m => m.PostId);

            builder.HasMany(p => p.Tags)
                   .WithMany(t => t.Posts)
                   .UsingEntity<PostTagBinding>(
                        j => j.HasOne(m => m.Tag).WithMany().HasForeignKey("TagId"),
                        j => j.HasOne(m => m.Post).WithMany().HasForeignKey("PostId"), j => j.ToTable("PostTagaaa"));
        }
    }

    public class TagMapping : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tag");
        }
    }

    public class OutPost : Post
    {

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
        public string TagId { get; set; }
        public string TagName { get; set; }

        public ICollection<OutPost> Posts { get; set; }
    }

    public class PostTagBinding
    {
        public int Id { get; set; }

        public OutPost Post { get; set; }

        public Tag Tag { get; set; }
    }
}
