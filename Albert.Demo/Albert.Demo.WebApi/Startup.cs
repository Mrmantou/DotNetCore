using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Albert.Demo.Application.Friends;
using Albert.Demo.Application.UrlNavs;
using Albert.Demo.EntityFramework.Sqlite;
using Albert.Demo.WebApi.Middleware;
using Albert.Domain.Repositories;
using Albert.Domain.Uow;
using Albert.EntityFrameworkCore.Uow;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Albert.Demo.WebApi
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
            services.AddDbContext<DemoContext>(options => options.UseSqlite(Configuration.GetConnectionString("DemoDefault")));

            services.AddScoped<IUnitOfWork, UnitOfWork<DemoContext>>();

            services.AddTransient(typeof(IRepository<>), typeof(DemoRepository<>));
            services.AddTransient(typeof(IRepository<,>), typeof(DemoRepository<,>));

            services.AddTransient<IFriendAppService, FriendAppService>();
            services.AddTransient<IUrlNavAppService, UrlNavAppService>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Albert Demo WebApi",
                    Version = "v1",
                    Description = "Albert Demo WebApi Open API",
                });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Albert Demo WebApi API v1");
            });

            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseMiddleware<LogMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
