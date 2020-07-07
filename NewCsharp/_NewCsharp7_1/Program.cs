using System;
using System.Threading;
using System.Threading.Tasks;

namespace _NewCsharp7_1
{
    /// <summary>
    /// What's new in C# 7.1
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-1
    /// </summary>
    class Program
    {
        /// <summary>
        /// Async main
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            #region Default literal expressions
            Func<string, bool> whereClause = default;
            #endregion

            await Task.Delay(100);

            Console.WriteLine("Hello World!");
        }

        private void InferredTupleElementNames()
        {
            int count = 5;
            string label = "Colors used in the map";
            var pair = (count, label);
        }
    }
}
