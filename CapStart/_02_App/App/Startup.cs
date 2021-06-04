using App.Diagnostics;
using App.EventHandler;
using App.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private readonly IConfiguration configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<DateTimeHandler>();

            services.AddCap(x =>
            {
                // If you are using EF, you need to add the configuration£º
                x.UseEntityFramework<AppDbContext>();
                x.UseRabbitMQ(options =>
                {
                    options.HostName = configuration["Cap:EventBusConnection"];
                    options.Port = int.Parse(configuration["Cap:EventBusPort"]);
                    if (!string.IsNullOrEmpty(configuration["Cap:EventBusUserName"]))
                    {
                        options.UserName = configuration["Cap:EventBusUserName"];
                    }
                    if (!string.IsNullOrEmpty(configuration["Cap:EventBusPassword"]))
                    {
                        options.Password = configuration["Cap:EventBusPassword"];
                    }
                });
            });

            services.AddSingleton<TracingDiagnosticProcessorObserver>();
            services.AddSingleton<IHostedService, DiagnosticTask>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "App", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App v1"));
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
