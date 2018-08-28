using System;

namespace BuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Director director = new Director();

            Builder builder1 = new ConcreteBuilder1();
            Builder builder2 = new ConcreteBuilder2();

            director.Construct(builder1);

            Computer computer1 = builder1.GetComputer();
            computer1.Show();
            
            director.Construct(builder2);
            Computer computer2 = builder2.GetComputer();
            computer2.Show();

            Console.WriteLine("press any key to exit...");
            Console.ReadKey();
        }
    }
}
