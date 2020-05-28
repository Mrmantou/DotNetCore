using System;
using System.Collections.Generic;

namespace BuilderPattern
{
    /// <summary>
    /// 电脑类
    /// </summary>
    public class Computer
    {
        private List<string> parts = new List<string>();

        public void Add(string part)
        {
            parts.Add(part);
        }

        public void Show()
        {
            Console.WriteLine("电脑开始在组装...");

            parts.ForEach(x => Console.WriteLine($"组件 {x} 已装好"));

            Console.WriteLine("电脑组装好了");
        }
    }
}