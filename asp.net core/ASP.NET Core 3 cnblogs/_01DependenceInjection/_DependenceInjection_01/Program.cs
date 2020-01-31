using System;
using System.Threading.Tasks;

namespace _DependenceInjection_01
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                var address = new Uri("http://0.0.0.0:8080/mvcapp");
                await MvcLib.ListenAsync(address);

                while (true)
                {
                    var request = await MvcLib.ReceiveAsync();
                    var controller = await MvcLib.CreateControllerAsync(request);
                    var view = await MvcLib.ExecuteControllerAsync(controller);
                    await MvcLib.RenderViewAsync(view);
                }
            }
        }
    }
}
