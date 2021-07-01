using Elsa.Activities.Console;
using Elsa.Builders;

namespace _01_ElsaConsole
{
    public class HelloWorld : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)=>builder.WriteLine("Hello World!");
    }
}
