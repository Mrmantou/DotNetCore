using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _Start_03_MVC
{
    public class SayHelloController
    {
        [HttpGet]
        [Route("/hello")]
        public string SayHello() => "Hello World";

    }
}
