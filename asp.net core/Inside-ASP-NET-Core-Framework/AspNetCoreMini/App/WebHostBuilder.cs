using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public interface IWebHostBuilder
    {
        IWebHostBuilder UseServer(IServer server);
        IWebHostBuilder Configure(Action<IApplicationBuilder> configure);
        IWebHost Build();
    }

    public class WebHostBuilder : IWebHostBuilder
    {
        private IServer server;
        private readonly List<Action<IApplicationBuilder>> configures = new List<Action<IApplicationBuilder>>();
        public IWebHostBuilder Configure(Action<IApplicationBuilder> configure)
        {
            configures.Add(configure);
            return this;
        }

        public IWebHostBuilder UseServer(IServer server)
        {
            this.server = server;
            return this;
        }

        public IWebHost Build()
        {
            var builder = new ApplicationBuilder();

            foreach (var item in configures)
            {
                item(builder);
            }

            return new WebHost(server, builder.Build());
        }
    }
}
