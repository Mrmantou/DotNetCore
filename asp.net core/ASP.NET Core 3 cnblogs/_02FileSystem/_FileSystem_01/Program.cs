using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Reflection;
using System.Text;
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

            Console.WriteLine("\nFile Change Watch:");
            await FileChange();

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

        static async Task FileChange()
        {
            var path = AppContext.BaseDirectory;
            IFileProvider fileProvider = new PhysicalFileProvider(path);
            ChangeToken.OnChange(() => fileProvider.Watch("test.txt"), () => LoadFileAsync(fileProvider));

            int i = 10;
            while (i-- > 0)
            {
                File.WriteAllText("test.txt", DateTime.Now.ToString());
                Task.Delay(5000).Wait();
            }
        }

        private static async void LoadFileAsync(IFileProvider fileProvider)
        {
            using (Stream stream = fileProvider.GetFileInfo("test.txt").CreateReadStream())
            {
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);
                Console.WriteLine(Encoding.ASCII.GetString(buffer));
            }
        }
    }
}
