using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class ContentResult : IActionResult
    {
        private readonly string content;
        private readonly string contentType;

        public ContentResult(string content, string contentType)
        {
            this.content = content;
            this.contentType = contentType;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = contentType;
            return response.WriteAsync(content);
        }
    }
}
