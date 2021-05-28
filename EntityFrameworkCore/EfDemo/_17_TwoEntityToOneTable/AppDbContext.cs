using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _17_TwoEntityToOneTable
{
    public class AppDbContext : DbContext
    {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=EF._17_TwoEntityToOneTable;";

        public DbSet<Article> Articles { get; set; }
        public DbSet<Novel> Novels { get; set; }

        public DbSet<DetailedOrder> DetailedOrders { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetailedOrder>(
                dob =>
                {
                    dob.ToTable("Orders");
                    dob.Property(o => o.Status).HasColumnName("Status");
                });

            modelBuilder.Entity<Order>(
                ob =>
                {
                    ob.ToTable("Orders");
                    ob.HasKey(o => o.Id);
                    ob.Property(o => o.Id).ValueGeneratedOnAdd();
                    ob.Property(o => o.Status).HasColumnName("Status");
                    ob.HasOne(o => o.DetailedOrder).WithOne()
                        .HasForeignKey<DetailedOrder>(o => o.Id);
                });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public class ArticleMapping : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Article");
            builder.HasKey(m => m.ArticleId);

            builder.Property(m => m.Title).HasColumnName("Title");
        }
    }

    public class NovelMapping : IEntityTypeConfiguration<Novel>
    {
        public void Configure(EntityTypeBuilder<Novel> builder)
        {
            builder.ToTable("Article");
            builder.HasKey(m => m.ArticleId);
            builder.Property(m => m.Title).HasColumnName("Title");

            builder.HasOne(m => m.Article)
                   .WithOne()
                   .HasForeignKey<Article>(m => m.ArticleId);
        }
    }

    public class Article
    {
        public string ArticleId { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }

        public string Author { get; set; }
        public string No { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{ArticleId} - {Title} - {Author} - {No} - {Price}";
        }

    }

    public class Novel
    {
        public string ArticleId { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public Article Article { get; set; }

        public override string ToString()
        {
            return $"{ArticleId} - {Title}";
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public DetailedOrder DetailedOrder { get; set; }
    }

    public class DetailedOrder
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public byte[] Version { get; set; }
    }
}
