using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Albert.Demo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemInfoController : ControllerBase
    {
        [HttpGet]
        public string PlatformAndFrameworkInfo()
        {
            return $"{RuntimeInformation.FrameworkDescription} on {RuntimeInformation.OSDescription}";
        }
    }
}
