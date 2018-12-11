using Albert.Demo.Application.UrlNavs;
using Albert.Demo.Application.UrlNavs.Dtos;
using Albert.Demo.Domain.UrlNavs;
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
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}