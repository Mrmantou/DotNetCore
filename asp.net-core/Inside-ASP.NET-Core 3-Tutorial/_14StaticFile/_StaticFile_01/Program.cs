using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace _StaticFile_01
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "doc");
            var fileProvider = new PhysicalFileProvider(path);

            var fileOptions = new StaticFileOptions
            {
                FileProvider = fileProvider,
                RequestPath = "/documents"
            };

            var directoryOptions = new DirectoryBrowserOptions
            {
                FileProvider = fileProvider,
                RequestPath = "/documents"
            };

            var defaultOptions = new DefaultFilesOptions
            {
                FileProvider = fileProvider,
                RequestPath = "/documents"
            };

            defaultOptions.DefaultFileNames.Add("readme.html");

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .Configure(app => app
                        .UseDefaultFiles()
                        .UseDefaultFiles(defaultOptions)
                        .UseStaticFiles()
                        .UseStaticFiles(fileOptions)
                        .UseDirectoryBrowser()
                        .UseDirectoryBrowser(directoryOptions)))
                .Build()
                .Run();
        }
    }
}
