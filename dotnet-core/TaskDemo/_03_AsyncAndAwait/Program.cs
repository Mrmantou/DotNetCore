using System;

namespace _03_AsyncAndAwait
{
    /**
     * var result = await expression;
     * statement(s);
     * 
     * Equal:
     * var awaiter = expression.GetAwaiter();
     * awaiter.OnCompleted(() => {
     *      var result = awaiter.GetResult();
     *      statement(s);
     * });
     **/
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
