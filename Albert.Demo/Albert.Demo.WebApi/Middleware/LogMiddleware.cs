using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Albert.Demo.WebApi.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await next(context);
            if (context.Response.StatusCode != (int)HttpStatusCode.OK)
            {
                logger.LogError($"request status code: {context.Response.StatusCode} request: {context.Request.Method} {context.Request.Host.Value}{context.Request.Path}");
            }
        }
    }
}
