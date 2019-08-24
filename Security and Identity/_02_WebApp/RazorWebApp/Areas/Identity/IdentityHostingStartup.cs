using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorWebApp.Models;

[assembly: HostingStartup(typeof(RazorWebApp.Areas.Identity.IdentityHostingStartup))]
namespace RazorWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<RazorWebAppContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("RazorWebAppContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<RazorWebAppContext>();
            });
        }
    }
}