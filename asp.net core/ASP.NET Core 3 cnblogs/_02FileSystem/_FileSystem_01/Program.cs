using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;

namespace _FileSystem_01
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderTreeStructure();

            Console.WriteLine("press any key to exit......");
            Console.ReadKey();
        }

        static void RenderTreeStructure()
        {
            static void Print(int layer, string name) => Console.WriteLine($"{new string(' ', layer * 4)}{name}");
            var path = AppContext.BaseDirectory;

            new ServiceCollection()
                .AddSingleton<IFileProvider>(new PhysicalFileProvider(path))
                .AddSingleton<IFileManager, FileManager>()
                .BuildServiceProvider()
                .GetRequiredService<IFileManager>()
                .ShowStructure(Print);
        }
    }
}
