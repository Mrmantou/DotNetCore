using Albert.Demo.Application.UrlNavs;
using Albert.Demo.Application.UrlNavs.Dtos;
using Albert.Demo.Domain.UrlNavs;
using Albert.Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albert.Demo.Controllers
{
    public class UrlNavController : Controller
    {
        private readonly IUrlNavAppService urlNavAppService;
        public UrlNavController(IUrlNavAppService urlNavAppService)
        {
            this.urlNavAppService = urlNavAppService;
        }

        // GET: UrlNav
        public async Task<ActionResult> Index()
        {
            var urlNavs = await urlNavAppService.GetUrlNavs(new GetUrlNavArg());

            return View(urlNavs);
        }

        // GET: UrlNav/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: UrlNav/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Title,Classify,Url,Description")] UrlNav urlNav)
        {
            try
            {
                await urlNavAppService.Create(urlNav);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UrlNav/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urlNav = await urlNavAppService.GetUrlNavs(new GetUrlNavArg { Id = id.Value });

            if (urlNav == null || urlNav.Count() == 0)
            {
                return NotFound();
            }

            return View(urlNav.First());
        }

        // POST: UrlNav/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, [Bind("Id,Title,Classify,Url,Description")] UrlNav urlNav)
        {
            if (id != urlNav.Id)
            {
                return NotFound();
            }

            try
            {
                await urlNavAppService.Update(urlNav);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urlNav = await urlNavAppService.GetUrlNavs(new GetUrlNavArg { Id = id.Value });

            if (urlNav == null || urlNav.Count() == 0)
            {
                return NotFound();
            }

            return View(new UrlNavDeleteViewModel { UrlNav = urlNav.First() });
        }

        // POST: Default/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, string token)
        {
            if (token != "@####@")
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await urlNavAppService.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}