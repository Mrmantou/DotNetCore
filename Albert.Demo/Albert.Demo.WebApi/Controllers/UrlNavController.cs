using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Albert.Demo.Application.UrlNavs;
using Albert.Demo.Application.UrlNavs.Dtos;
using Albert.Demo.Domain.UrlNavs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Albert.Demo.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UrlNavController : ControllerBase
    {
        private readonly IUrlNavAppService urlNavAppService;
        public UrlNavController(IUrlNavAppService urlNavAppService)
        {
            this.urlNavAppService = urlNavAppService;
        }

        [HttpGet]
        public async Task<List<UrlNav>> Get(string classify, string title, Guid id)
        {
            var result = await urlNavAppService.GetUrlNavs(new GetUrlNavArg
            {
                Id = id,
                Title = title,
                Classify = classify
            });

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UrlNav urlNav)
        {
            urlNav.CreateTime = DateTime.Now;
            urlNav.UpdateTime = DateTime.Now;
            await urlNavAppService.Create(urlNav);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UrlNav urlNav)
        {
            urlNav.UpdateTime = DateTime.Now;
            await urlNavAppService.Update(urlNav);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id, string token)
        {
            if (token != "@####@")
            {
                return Forbid();
            }

            await urlNavAppService.Delete(id);

            return Ok();
        }


        // GET: UrlNav/Time
        /// <summary>
        /// 查看创建和修改时间列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Time()
        {
            var urlNavs = await urlNavAppService.GetUrlNavs(new GetUrlNavArg());
            return Ok(from urlNav in urlNavs
                      select new
                      {
                          urlNav.Title,
                          urlNav.CreateTime,
                          urlNav.UpdateTime
                      });
        }
    }
}
