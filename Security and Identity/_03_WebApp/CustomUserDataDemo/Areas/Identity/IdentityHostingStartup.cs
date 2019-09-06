using System;
using CustomUserDataDemo.Areas.Identity.Data;
using CustomUserDataDemo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CustomUserDataDemo.Areas.Identity.IdentityHostingStartup))]
namespace CustomUserDataDemo.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CustomUserDataDemoContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CustomUserDataDemoContextConnection")));

                services.AddDefaultIdentity<CustomUser>()
                    .AddEntityFrameworkStores<CustomUserDataDemoContext>();
            });
        }
    }
}