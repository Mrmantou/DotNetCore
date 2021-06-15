using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01_QuickCap.Controllers
{
    public class PublishController : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("~/send")]
        public IActionResult SendMessage([FromServices] ICapPublisher capBus)
        {
            capBus.Publish("test.show.time", DateTime.Now);

            return Ok();
        }
    }
}
