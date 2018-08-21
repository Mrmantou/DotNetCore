using Microsoft.AspNetCore.Http;
using MiddlewareExtensibilitySample.Data;
using MiddlewareExtensibilitySample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExtensibilitySample.Middleware
{
    public class SimpleInjectorActivatedMiddleware : IMiddleware
    {
        private readonly AppDbContext db;

        public SimpleInjectorActivatedMiddleware(AppDbContext db)
        {
            this.db = db;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var keyValue = context.Request.Query["key"];

            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                db.Add(new Request
                {
                    DT = DateTime.Now,
                    MiddlewareActivation = "SimpleInjectorActivatedMiddleware",
                    Value = keyValue
                });

                await db.SaveChangesAsync();
            }

            await next(context);
        }
    }
}
