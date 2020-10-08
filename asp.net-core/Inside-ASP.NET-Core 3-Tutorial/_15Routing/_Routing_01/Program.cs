using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _Routing_01
{
    class Program
    {
        private static Dictionary<string, string> cities = new Dictionary<string, string>
        {
            ["010"] = "Beijing",
            ["028"] = "Chengdu",
            ["0512"] = "Suzhou"
        };

        public static async Task WeatherForecast(HttpContext context)
        {
            var city = (string)context.GetRouteData().Values["city"];
            city = cities[city];
            int days = int.Parse(context.GetRouteData().Values["days"].ToString());
            var report = new WeatherReport(city, days);

            await RendWeatherAsync(context, report);
        }

        public static async Task WeatherForecastWithDefaultValue(HttpContext context)
        {
            var routeValues = context.GetRouteData().Values;
            var city = routeValues.TryGetValue("city", out var v1) ? (string)v1 : "010";
            city = cities[city];

            int days = routeValues.TryGetValue("days", out var v2) ? int.Parse(v2.ToString()) : 4;
            var report = new WeatherReport(city, days);

            await RendWeatherAsync(context, report);
        }

        private static async Task RendWeatherAsync(HttpContext context, WeatherReport report)
        {
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync("<html><head><title>Weather</title></head><body>");
            await context.Response.WriteAsync($"<h3>{report.City}</h3>");

            foreach (var item in report.WeatherInfos)
            {
                await context.Response.WriteAsync($"{item.Key:yyyy-MM-dd}:");
                await context.Response.WriteAsync($"{item.Value.Condition}({item.Value.LowTemperature}°C ~ {item.Value.HighTemperature}°C)<br/><br/>");
            }

            await context.Response.WriteAsync("</body></html>");
        }

        static void Main(string[] args)
        {
            // var template = @"weather/{city}/{days}";
            // var template = @"weather/{city:regex(^0\d{{2,3}}$)}/{days:int:range(1,4)}"; // inline constraint
            var defaultTemplate = @"weather/{city?}/{days?}";

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(service => service.AddRouting())
                    .Configure(app => app
                        .UseRouting()
                        .UseEndpoints(endpoints =>
                            endpoints.MapGet(defaultTemplate, WeatherForecastWithDefaultValue)))) //weather/{city}/{days}
                .Build()
                .Run();
        }
    }
}
