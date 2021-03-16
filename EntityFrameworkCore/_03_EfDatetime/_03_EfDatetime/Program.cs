using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _03_EfDatetime
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new BloggingContext();

            //context.Blogs.Add(new Blog { CreateTime = DateTime.Now, UpdateTime = DateTime.Now, Url = "www.baidu.com" });
            //await context.SaveChangesAsync();

            var blogs = await context.Blogs.ToListAsync();

            Console.WriteLine("Hello World!");
        }
    }

    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=(LocalDb)\MSSQLLocalDB;Database=Blogging;Trusted_Connection=True;MultipleActiveResultSets=true");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogMapping());

        }
    }

    public class BlogMapping : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable(nameof(Blog));
            builder.HasKey(b => b.BlogId);
            builder.Property(b => b.BlogId).ValueGeneratedOnAdd();
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        private DateTime createDate = DateTime.Now;
        public DateTime CreateTime
        {
            get => createDate;
            set
            {
                createDate = new DateTime(value.Year, value.Month, value.Day);
            }
        }
        public DateTime UpdateTime { get; set; }
    }
}
