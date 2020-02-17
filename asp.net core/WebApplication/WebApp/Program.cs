using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Host负责应用的启动和生存期管理，配置服务器和请求处理管道
        // 默认设置日志记录、依赖关系的注入和配置
        // Host是一个封装了应用资源的对象
        // .net core (核心api)
        // .net extensions 源码 (.net core 扩展包)
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // .net core 有两种host，泛型(通用)host
            // web host (通用host的扩展，提供额外web功能，支持http，集成了kestrel，内置IIS集成)
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // 加载前缀为 ASPNETCORE_ 的环境变量加载web host配置
                    // 默认将kestrel设置为web服务器并对其进行默认配置/支持IIS集成
                    // 自定义配置 关于web host

                    // 组件配置，不属于host 由host调用(扩展类提供的方法)
                    // webBuilder.ConfigureKestrel(options=> options.Limits)
                    // webBuilder.ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Debug));

                    webBuilder.UseStartup<Startup>();
                });
    }
}
