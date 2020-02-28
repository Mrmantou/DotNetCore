using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace _FileSystem_01
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("File Tree:");
            RenderTreeStructure();

            Console.WriteLine("\nFile Content:");
            await ReadFileContent();

            Console.WriteLine("\nEmbedded File:");
           await EmbeddedFile();

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

        static async Task ReadFileContent()
        {
            var path = AppContext.BaseDirectory;

            var content = await new ServiceCollection()
                  .AddSingleton<IFileProvider>(new PhysicalFileProvider(path))
                  .AddSingleton<IFileManager, FileManager>()
                  .BuildServiceProvider()
                  .GetRequiredService<IFileManager>()
                  .ReadAllTextAsync("_FileSystem_01.runtimeconfig.json");

            Console.WriteLine(content);
        }

        static async Task EmbeddedFile()
        {
            var assembly = Assembly.GetEntryAssembly();

            var content = await new ServiceCollection()
                  .AddSingleton<IFileProvider>(new EmbeddedFileProvider(assembly))
                  .AddSingleton<IFileManager, FileManager>()
                  .BuildServiceProvider()
                  .GetRequiredService<IFileManager>()
                  .ReadAllTextAsync("data.txt");

            Console.WriteLine(content);
        }
    }
}
