using System;

namespace TaskYieldDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            new AwaitYieldDemo().MockHotPotRestaurant();

            Console.WriteLine("press any key to exit......");
            Console.ReadKey();
        }
    }
}
