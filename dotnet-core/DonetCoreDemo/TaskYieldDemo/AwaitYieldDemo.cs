using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskYieldDemo
{
    public class AwaitYieldDemo
    {
        public void MockHotPotRestaurant()
        {
            Task[] tasks = new Task[20];
            //构建一批吃火锅的人
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(PersonEatHot, i);
            }
            //人们陆续来吃火锅
            for (int j = 0; j < tasks.Length / 2; j++)
            {
                tasks[j].Start();
            }
            Console.WriteLine($"main thread ---{Thread.CurrentThread.ManagedThreadId}");
            //我来吃火锅了
            GotoShuDaXiaEatHotPot();
            //人们陆续来吃火锅
            for (int j = 10; j < tasks.Length; j++)
            {
                tasks[j].Start();
            }
        }
        private void PersonEatHot(object personNo)
        {
            Console.WriteLine($"I am No.{personNo} person.I enter restaurant ---{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);   //eating
            Console.WriteLine($"I am No.{ personNo } person.I eat completed. ---{Thread.CurrentThread.ManagedThreadId}");
        }
        private async Task GotoShuDaXiaEatHotPot()
        {
            Console.WriteLine($"I get a waiting card. ---{Thread.CurrentThread.ManagedThreadId}");
            await Task.Yield();  //到店了 先排个号 Creates an awaitable task that asynchronously yields back to the current context when awaited.
            WaitMyPartnerJoin(5);  //等待我的5个小伙伴集合
            await EatingHotPot();   //开始吃火锅
        }
        private async Task EatingHotPot()
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"eating hot pot with my friends ---{Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
                Console.WriteLine($"Completed : eating hot pot with my friends ---{Thread.CurrentThread.ManagedThreadId}");
            });
        }
        private void WaitMyPartnerJoin(int partnerNum)
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Waiting my partner join.");
            for (int i = 0; i < partnerNum; i++)
            {
                for (int j = 0; j < 1000000; j++)
                {
                }
                Console.WriteLine($"no.{i} friend join. ---{Thread.CurrentThread.ManagedThreadId}");
            }
            Console.WriteLine($"everyone is here. ---{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
