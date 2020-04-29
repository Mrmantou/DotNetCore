using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(services => services
                        .AddSingleton<ICompiler, Compiler>()
                        .AddSingleton<DynamicChangeTokenProvider>()
                        .AddSingleton<IActionDescriptorChangeProvider>(provider => provider.
                            GetRequiredService<DynamicChangeTokenProvider>())
                        .AddRouting()
                        .AddControllersWithViews())
                    .Configure(app => app
                        .UseRouting()
                        .UseEndpoints(endpoints => endpoints
                            .MapControllerRoute(
                                name: default,
                                pattern: "{controller}/{action}"))))
                .Build()
                .Run();
        }
    }
}
