using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    /// <summary>
    /// 按照约定类创建
    /// 具有类型为RequestDelegate的参数的公共构造函数
    /// 名为Invoke或者InvokeAsync的公共方法，这个方法必须满足两个条件：
    ///     返回Task
    ///     接受类型HttpContext的第一个参数
    /// </summary>
    public class TestMiddleware
    {
        private RequestDelegate next;
        public TestMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// 中间件功能代码
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync($"Message:{httpContext.Request.Path}");
            // 可以选择是否调用next
            // 如果不调用，那么这个中间件会触发短路，终端中间件
            await next(httpContext);
        }
    }
}
