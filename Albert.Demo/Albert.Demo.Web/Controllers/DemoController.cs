using Microsoft.AspNetCore.Mvc;

namespace Albert.Demo.Web.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}