using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace _01_ElsaConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddWorkflow<HelloWorld>())
                .BuildServiceProvider();

            var workflowRunner = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            await workflowRunner.BuildAndStartWorkflowAsync<HelloWorld>();

            Console.WriteLine("press any key to exit......");
        }
    }
}
