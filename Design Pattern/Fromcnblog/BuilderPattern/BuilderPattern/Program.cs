using System;

namespace BuilderPattern
{
    /// <summary>
    /// 介绍完了建造者模式的具体实现之后，让我们总结下建造模式的实现要点：
    /// 1.在建造者模式中，指挥者是直接与客户端打交道的，指挥者将客户端创建产品的请求划分为对各个部件的建造请求，
    ///   再将这些请求委派到具体建造者角色，具体建造者角色是完成具体产品的构建工作的，却不为客户所知道。
    /// 2.建造者模式主要用于“分步骤来构建一个复杂的对象”，其中“分步骤”是一个固定的组合过程，
    ///   而复杂对象的各个部分是经常变化的（也就是说电脑的内部组件是经常变化的，这里指的的变化如硬盘的大小变了，CPU由单核变双核等）。
    /// 3.产品不需要抽象类，由于建造模式的创建出来的最终产品可能差异很大，所以不大可能提炼出一个抽象产品类。
    /// 4.在前面文章中介绍的抽象工厂模式解决了“系列产品”的需求变化，而建造者模式解决的是 “产品部分” 的需要变化。
    /// 5.由于建造者隐藏了具体产品的组装过程，所以要改变一个产品的内部表示，只需要再实现一个具体的建造者就可以了，从而能很好地应对产品组成组件的需求变化。
    /// </summary>
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
