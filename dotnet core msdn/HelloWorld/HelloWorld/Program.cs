using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nWhat is your name?");
            var name = Console.ReadLine();
            var date = DateTime.Now;

            Console.WriteLine($"\nHello, {name}, on {date:d} at {date:t}!");

            Console.WriteLine("Hello World!");

            Console.WriteLine("Press any key to continue...");

            Console.ReadKey(true);
        }
    }
}
