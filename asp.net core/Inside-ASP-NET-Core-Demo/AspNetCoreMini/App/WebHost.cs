using System.Threading.Tasks;

namespace App
{
    public interface IWebHost
    {
        Task StartAsync();
    }
    public class WebHost : IWebHost
    {
        private readonly IServer server;
        private readonly RequestDelegate handler;

        public WebHost(IServer server, RequestDelegate handler)
        {
            this.server = server;
            this.handler = handler;
        }

        public Task StartAsync() => server.StartAsync(handler);
    }
}
