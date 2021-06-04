using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01_QuickCap.Controllers
{
    public class ConsumerController : Controller
    {
        [NonAction]
        [CapSubscribe("test.show.time")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public void ReceiveMessage(DateTime time)
        {
            Console.WriteLine("message time is:" + time);
        }
    }
}
