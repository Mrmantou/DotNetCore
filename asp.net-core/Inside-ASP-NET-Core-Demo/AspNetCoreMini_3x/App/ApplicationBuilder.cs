using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App
{
    public interface IApplicationBuilder
    {
        RequestDelegate Build();
        IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);
    }

    public class ApplicationBuilder : IApplicationBuilder
    {
        private readonly List<Func<RequestDelegate, RequestDelegate>> middlewares = new List<Func<RequestDelegate, RequestDelegate>>();

        public RequestDelegate Build()
        {
            middlewares.Reverse();

            return httpContext =>
            {
                RequestDelegate next = _ => { _.Response.StatusCode = 404; return Task.CompletedTask; };

                foreach (var middleware in middlewares)
                {
                    next = middleware(next);
                }
                return next(httpContext);
            };
        }

        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            middlewares.Add(middleware);
            return this;
        }
    }
}
