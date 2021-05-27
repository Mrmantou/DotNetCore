using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _16_ManyToMany
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var article = new Article { Title = "C#" };


            var article_1 = new Article { Title = "C# 01" };
            var article_2 = new Article { Title = "C# 02" };
            using (var context = new AppDbContext())
            {
                await context.Articles.AddAsync(article);

                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext())
            {
                article = context.Articles.First(x => x.Title == "C#");
                article.RelatedArticles.AddRange(new List<Article> {
                    article_1, article_2
                });

                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext())
            {
                article = context.Articles.Include(a => a.RelatedArticles).First(x => x.ArticleId == article.ArticleId);
            };

            Console.WriteLine("Hello World!");
        }
    }
}
