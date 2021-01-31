using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSimple
{
    // Create Task
    class Program
    {
        static void Main(string[] args)
        {
            TaskScheduler.UnobservedTaskException += TaskSchedulerUnobservedTaskExceptionHandle; // subscribe global unhandled task exception

            Console.WriteLine("press any key to exit...");
            Console.ReadKey();
        }

        private static void TaskSchedulerUnobservedTaskExceptionHandle(object sender, UnobservedTaskExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        static void Test1()
        {
            Task.Run(() => Console.WriteLine("Foo"));
        }

        static void Test2()
        {
            var task = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Foo");
            });

            task.Wait();
        }

        static void Test3()
        {
            var task = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Foo");
            }, TaskCreationOptions.LongRunning);
        }

        static void Test4()
        {
            Task<int> task = Task.Run(() =>
            {
                Console.WriteLine("Foo");
                return 3;
            });

            int result = task.Result; // block if not already finished
            Console.WriteLine(result);
        }

        static void Test5()
        {
            Task<int> primeNumberTask = Task.Run(() =>
                Enumerable.Range(2, 3000000).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));

            Console.WriteLine("task running...");
            Console.WriteLine("The answer is " + primeNumberTask.Result);
        }

        static void Test6()
        {
            var task = Task.Run(() => { throw null; }); // start a Task that throws a NullReferenceException

            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is NullReferenceException)
                {
                    Console.WriteLine("Null!");
                }
                else
                {
                    throw;
                }
            }
        }

        static void Test7()
        {
            Task<int> primeNumberTask = Task.Run(() =>
                Enumerable.Range(2, 3000000).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));

            var awaiter = primeNumberTask.GetAwaiter();

            awaiter.OnCompleted(() =>
            {
                var result = awaiter.GetResult();
                Console.WriteLine("The answer is " + result);
            });
        }

        static void Test8()
        {
            Task<int> primeNumberTask = Task.Run(() =>
                Enumerable.Range(2, 3000000).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));

            var awaiter = primeNumberTask.ConfigureAwait(false) // configure whether attempt to marshal the continuation back to the original context captured
                                         .GetAwaiter();

            awaiter.OnCompleted(() =>
            {
                var result = awaiter.GetResult();
                Console.WriteLine("The answer is " + result);
            });
        }

        static void Test9()
        {
            Task<int> primeNumberTask = Task.Run(() =>
                Enumerable.Range(2, 3000000).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));

            primeNumberTask.ContinueWith(antecedent =>
            {
                var result = antecedent.Result;
                Console.WriteLine("The answer is " + result);
            });
        }

        static void Test10()
        {
            var tcs = new TaskCompletionSource<int>();
            new Thread(() =>
            {
                Thread.Sleep(5000);
                tcs.SetResult(42);
            }).Start();

            Task<int> task = tcs.Task;
            Console.WriteLine(task.Result);
        }

        static void Test11()
        {
            var awaiter = MyTask.GetAnswerToLife().GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                Console.WriteLine(awaiter.GetResult());
            });
        }

        static void Test12()
        {
            MyTask.Delay(5000).GetAwaiter().OnCompleted(() =>
            {
                Console.WriteLine();
            });
        }

        static void Test13()
        {
            Task.Delay(5000).GetAwaiter().OnCompleted(() =>
            {
                Console.WriteLine(42);
            });

            Task.Delay(5000).ContinueWith(ant =>
            {
                Console.WriteLine();
            });
        }
    }

    class MyTask
    {
        public Task<TResult> Run<TResult>(Func<TResult> function)
        {
            var tcs = new TaskCompletionSource<TResult>();

            new Thread(() =>
            {
                try
                {
                    tcs.SetResult(function());
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }).Start();

            return tcs.Task;
        }

        public static Task<int> GetAnswerToLife()
        {
            var tcs = new TaskCompletionSource<int>();

            var timer = new System.Timers.Timer(5000) { AutoReset = false };
            timer.Elapsed += delegate
            {
                timer.Dispose();
                tcs.SetResult(42);
            };
            timer.Start();

            return tcs.Task;
        }

        public static Task Delay(int milliseconds)
        {
            var tcs = new TaskCompletionSource<object>();

            var timer = new System.Timers.Timer(milliseconds) { AutoReset = false };
            timer.Elapsed += delegate
            {
                timer.Dispose();
                tcs.SetResult(null);
            };
            timer.Start();

            return tcs.Task;
        }
    }
}
