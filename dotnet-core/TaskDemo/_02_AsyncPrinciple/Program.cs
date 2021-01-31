using System;
using System.Linq;
using System.Threading.Tasks;

namespace _02_AsyncPrinciple
{
    class Program
    {
        static async void Main(string[] args)
        {
            //// 1
            //PrimeNumberHelper.DisplayPrimeCounts();

            //// 2
            //Task.Run(() => PrimeNumberHelper.DisplayPrimeCounts());

            //// 3
            //PrimeNumberHelper.DisplayPrimeCountsWithAsyncMethod();

            //// 4
            //PrimeNumberHelper.DisplayPrimeCountsChain();

            // 5
            await PrimeNumberHelper.DisplayPrimeCountsAsync();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    class PrimeNumberHelper
    {
        public static void DisplayPrimeCounts()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(GetPrimesCount(i * 1000000 + 2, 1000000) + " primes between " + (i * 1000000) + " and " + ((i + 1) * 1000000 - 1));
            }
            Console.WriteLine("Done!");
        }

        public static void DisplayPrimeCountsWithAsyncMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                var awaiter = GetPrimesCountAsync(i * 1000000 + 2, 1000000).GetAwaiter();
                awaiter.OnCompleted(() => Console.WriteLine(awaiter.GetResult() + " primes between ......"));
            }
            Console.WriteLine("Done!");
        }

        public static void DisplayPrimeCountsChain()
        {
            DisplayPrimeCounts(0);
        }

        public static void DisplayPrimeCounts(int i)
        {
            var awaiter = GetPrimesCountAsync(i * 1000000 + 2, 1000000).GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                Console.WriteLine(awaiter.GetResult() + " primes between ......");
                if (++i < 10)
                {
                    DisplayPrimeCounts(i);
                }
                else
                {
                    Console.WriteLine("Done!");
                }
            });
        }

        public static Task DisplayPrimeCountsAsyncComplex()
        {
            var machine = new PrimesStateMachine();
            machine.DisplayPrimeCountsFrom(0);

            return machine.Task;
        }

        public static async Task DisplayPrimeCountsAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(await GetPrimesCountAsync(i * 1000000 + 2, 1000000) + " primes between " + (i * 1000000) + " and " + ((i + 1) * 1000000 - 1));
            }
            Console.WriteLine("Done!");
        }

        class PrimesStateMachine
        {
            private readonly TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            public Task Task => tcs.Task;

            public void DisplayPrimeCountsFrom(int i)
            {
                var awaiter = GetPrimesCountAsync(i * 1000000 + 2, 1000000).GetAwaiter();
                awaiter.OnCompleted(() =>
                {
                    Console.WriteLine(awaiter.GetResult() + " primes between ......");
                    if (++i < 10)
                    {
                        DisplayPrimeCountsFrom(i);
                    }
                    else
                    {
                        Console.WriteLine("Done!");
                        tcs.SetResult(null);
                    }
                });
            }
        }

        private static int GetPrimesCount(int start, int count)
        {
            return ParallelEnumerable.Range(start, count)
                .Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0));
        }

        private static Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() => ParallelEnumerable.Range(start, count)
              .Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }
    }
}
