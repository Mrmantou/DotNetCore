using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index() => View();

        [HttpPost("/")]
        public IActionResult Index(
            string source,
            [FromServices] DynamicActionProvider actionProvider,
            [FromServices] DynamicChangeTokenProvider tokenProvider)
        {
            try
            {
                actionProvider.AddControllers(source);
                tokenProvider.NotifyChanges();
                return Content("OK");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
