using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiApp.SettingModels;

namespace WebApiApp
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Action<WebSetting> webSetting = setting =>
            //{
            //    Configuration.GetSection("WebSetting").Bind(setting);
            //};
            //services.Configure(webSetting);

            services.Configure<AppSetting>(Configuration);

            // custom setting file
            var config = new ConfigurationBuilder()
                .AddJsonFile("CustomSetting.json")
                .Build();
            services.Configure<CustomSetting>(config);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSetting> appOptions, IOptions<CustomSetting> customOptions, ILogger<Startup> logger)
        {
            logger.LogDebug("hello debug");
            app.Run(async context =>
            {
                var connStr = Configuration["ConnectionString"];
                var title = Configuration["WebSetting:Title"];
                var isCheckIp = Configuration["WebSetting:Behavior:IsCheckIp"];

                //绑定到配置模型对象
                var appSetting = new AppSetting();
                Configuration.Bind(appSetting);

                //部分绑定
                var webSetting = new WebSetting();
                Configuration.GetSection("WebSetting").Bind(webSetting);

                var setting = appOptions.Value.WebSetting;
                var name = customOptions.Value.Name;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
