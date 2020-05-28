using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _Start_03_MVC
{
    public class HelloController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        [Route("/hello/{name}")]
        public IActionResult SayHello(string name)
        {
            ViewBag.Name = name;
            return View();
        }
    }
}
