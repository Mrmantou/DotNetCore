using System;
using System.Collections.Generic;

namespace _14_ManyToMany
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new AppDbContext();

            context.Posts.Add(new Post
            {
                Title = "123",
                Content = "123456789",
                Tags = new List<Tag>
                {
                    new Tag { TagName = "aaa" },
                    new Tag { TagName = "bbb" }
                }
            });


            context.SaveChanges();

            Console.WriteLine("Hello World!");
        }
    }
}
