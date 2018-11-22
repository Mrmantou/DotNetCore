using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.IO;

namespace ConsoleApp.SQLite
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/ef/core/get-started/netcore/new-db-sqlite
    /// nuget package:
    ///  - Microsoft.EntityFrameworkCore.Sqlite
    ///  - Microsoft.EntityFrameworkCore.Design
    ///  - Microsoft.EntityFrameworkCore.Tools
    ///  Run from Visual Studio
    ///  To run this sample from Visual Studio, you must set the working directory manually to be the root of the project. If you don't set the working directory, the following Microsoft.Data.Sqlite.SqliteException is thrown: SQLite Error 1: 'no such table: Blogs'.
    ///To set the working directory:
    /// - In Solution Explorer, right click the project and then select Properties.
    /// - Select the Debug tab in the left pane.
    /// - Set Working directory to the project directory.
    /// - Save the changes.
    /// 启用配置文件需要安装包：
    /// - Microsoft.Extensions.Configuration
    /// - Microsoft.Extensions.Configuration.Json
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Config.json")
                .Build();

            var connection = config.GetConnectionString("BloggingDatabase");

            //ILoggerFactory loggerFactory = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

            ILoggerFactory loggerFactory = new LoggerFactory(
                new[] {
                    new ConsoleLoggerProvider((category, level)
                        => category == DbLoggerCategory.Database.Command.Name && 
                            level == LogLevel.Information, true)
                });


            using (var context = new BloggingContext(connection, loggerFactory))
            {
                context.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                var count = context.SaveChanges();
                Console.WriteLine($"{count} records saved to database");

                Console.WriteLine();
                Console.WriteLine("All blogs in database:");

                foreach (var blog in context.Blogs)
                {
                    Console.WriteLine($" - {blog.Url}");
                }
            }

            Console.WriteLine("Hello World!");
            Console.WriteLine("press any key to exit...");
            Console.ReadKey();
        }
    }
}
