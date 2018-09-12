using System;
using System.Collections.Generic;
using System.Text;

namespace SingletonDemo
{
    /// <summary>
    /// 单线程Singleton
    /// </summary>
    public class Singleton
    {
        private static Singleton instance;

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }

                return instance;
            }
        }
    }
}
