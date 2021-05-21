using System;
using System.Linq;
using System.Threading.Tasks;

namespace _01_Ef
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new AppDbContext();

            // Note: This sample requires the database to be created before running.

            // Create
            Console.WriteLine("Inserting a new blog");
            await context.AddAsync(new Blog { Url = "http://blogs.msdn.com/adonet" });
            await context.SaveChangesAsync();

            // Read
            Console.WriteLine("Querying for a blog");
            var blog = context.Blogs
                .OrderBy(b => b.BlogId)
                .First();

            // Update
            Console.WriteLine("Updating the blog and adding a post");
            blog.Url = "https://devblogs.microsoft.com/dotnet";
            blog.Posts.Add(
                new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
            await context.SaveChangesAsync();

            // Delete
            //Console.WriteLine("Delete the blog");
            //context.Remove(blog);
            //await context.SaveChangesAsync();
        }
    } 
}
