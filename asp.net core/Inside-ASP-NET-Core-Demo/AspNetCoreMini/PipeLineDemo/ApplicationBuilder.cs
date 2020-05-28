using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLineDemo
{
    public class ApplicationBuilder
    {
        private readonly IList<Func<RequestDelegate, RequestDelegate>> components = new List<Func<RequestDelegate, RequestDelegate>>();

        public ApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            components.Add(middleware);
            return this;
        }

        public RequestDelegate Build()
        {
            RequestDelegate app = context =>
            {
                Console.WriteLine("default middleware......");
                return Task.CompletedTask;
            };

            foreach (var component in components.Reverse())
            {
                app = component(app);
            }

            return app;
        }
    }

    public static class UseExtention
    {
        public static ApplicationBuilder Use(this ApplicationBuilder app, Func<HttpContext, Func<Task>, Task> middleware)
        {
            return app.Use(next =>
            {
                return context =>
                {
                    Func<Task> simpleNext = () => next(context);
                    return middleware(context, simpleNext);
                };
            });
        }
    }

}
