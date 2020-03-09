using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class HttpListenerServer : IServer
    {
        private readonly HttpListener httpListener;
        private readonly string[] urls;

        public HttpListenerServer(params string[] urls)
        {
            httpListener = new HttpListener();
            this.urls = urls.Any() ? urls : new string[] { "http://localhost:5000/" };
        }

        public async Task StartAsync(RequestDelegate handler)
        {
            Array.ForEach(urls, url => httpListener.Prefixes.Add(url));
            httpListener.Start();

            Console.WriteLine($"Server started and is listening on: {string.Join(';', urls)}");

            while (true)
            {
                var listenerContext = await httpListener.GetContextAsync();
                var feature = new HttpListenerFeature(listenerContext);

                var features = new FeatureCollection()
                    .Set<IHttpRequestFeature>(feature)
                    .Set<IHttpResponseFeature>(feature);

                var httpContext = new HttpContext(features);

                await handler(httpContext);

                listenerContext.Response.Close();
            }
        }
    }
}
