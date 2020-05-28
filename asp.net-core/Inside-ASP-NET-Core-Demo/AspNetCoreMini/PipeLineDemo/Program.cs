using System;

namespace PipeLineDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ApplicationBuilder();

            var firstMiddleware = builder
                .Use(async (context, next) =>
                {
                    Console.WriteLine("First middleware begin......");
                    await next();
                    Console.WriteLine("First middleware end......");
                })
                .Use(async (context, next) =>
                {
                    Console.WriteLine("Second middleware begin......");
                    await next();
                    Console.WriteLine("Second middleware end......");
                })
                .Build();

            firstMiddleware(new HttpContext());

            Console.WriteLine("press any key to exit......");
            Console.ReadKey();
        }
    }
}
