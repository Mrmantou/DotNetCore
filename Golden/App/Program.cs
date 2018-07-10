using System;
using Library;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine($"The answer is {new Thing().Get(42)}.");
        }
    }
}
