using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _16_ManyToMany
{
    public class AppDbContext : DbContext
    {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=EF._16_ManyToMany;";

        public DbSet<Article> Articles { get; set; }

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

    public class ArticleRelateArticleMapping : IEntityTypeConfiguration<ArticleRelateArticle>
    {
        public void Configure(EntityTypeBuilder<ArticleRelateArticle> builder)
        {
            builder.ToTable("ArticleRelateArticle");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();
            builder.HasOne(m => m.Article).WithMany().HasForeignKey(m=>m.ArticleId);
            builder.HasOne(m => m.RelatedArticle).WithMany().HasForeignKey(m => m.RelatedArticleId);
        }
    }

    public class OutPostMapping : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Article");
            builder.HasKey(m => m.ArticleId);

            builder.HasMany(p => p.RelatedArticles)
                   .WithMany(t => t.RelatedArticles)
                   .UsingEntity<ArticleRelateArticle>(
                        j => j.HasOne(m => m.Article).WithMany().HasForeignKey(m => m.RelatedArticleId),
                        j => j.HasOne(m => m.RelatedArticle).WithMany().HasForeignKey(m => m.ArticleId));
        }
    }

    public class ArticleRelateArticle
    {
        public int Id { get; set; }
        public string ArticleId { get; set; }
        public Article Article { get; set; }

        public string RelatedArticleId { get; set; }
        public Article RelatedArticle { get; set; }
    }

    public class Article
    {
        public string ArticleId { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }

        public List<Article> RelatedArticles { get; set; } = new List<Article>();
    }
}
