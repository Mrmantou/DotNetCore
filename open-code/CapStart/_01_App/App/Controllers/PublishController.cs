using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("api/publish")]
    [ApiController]
    public class PublishController : Controller
    {
        private readonly ICapPublisher capBus;
        private readonly AppDbContext dbContext;

        public PublishController(ICapPublisher capPublisher, AppDbContext dbContext)
        {
            capBus = capPublisher;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Publish()
        {
            using (var trans = dbContext.Database.BeginTransaction(capBus, autoCommit: true))
            {
                //your business logic code

                capBus.Publish("xxx.services.show.time", DateTime.Now);
            }

            return Ok();
        }
    }
}
