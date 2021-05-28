using System;
using System.Linq;

namespace _18_TwoEntityToOneTable
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AppArticleContext())
            {
                context.Articles.Add(new Article { Title = "Hello world", Author = "Albert", No = "SD00001", Price = 49 });
                context.SaveChanges();
            }
            using (var context = new AppNovelContext())
            {
                context.Novels.Add(new Novel { Title = "C# is the bast language" });

                context.SaveChanges();
            }

            using (var context = new AppArticleContext())
            {
                context.Articles.ToList().ForEach(x => Console.WriteLine(x));
            }

            using (var context = new AppNovelContext())
            {
                context.Novels.ToList().ForEach(x => Console.WriteLine(x));
            }
            Console.WriteLine("Hello World!");
        }
    }
}
