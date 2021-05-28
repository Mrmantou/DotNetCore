using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _18_TwoEntityToOneTable
{
    public class AppNovelContext : DbContext
    {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=EF._18_TwoEntityToOneTable;";

        public DbSet<Novel> Novels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Novel>(ob =>
            {
                ob.ToTable("Article");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public class AppArticleContext : DbContext
    {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=EF._18_TwoEntityToOneTable;";

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(ob =>
            {
                ob.ToTable("Article");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
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

        public override string ToString()
        {
            return $"{ArticleId} - {Title}";
        }
    }
}
