using Albert.Demo.Application.Friends;
using Albert.Demo.Application.Friends.Dtos;
using Albert.Demo.Domain.Friends;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albert.Demo.Controllers
{
    public class FriendController : Controller
    {
        private readonly IFriendAppService friendAppService;

        public FriendController(IFriendAppService friendAppService)
        {
            this.friendAppService = friendAppService;
        }

        // GET: Friend
        public async Task<ActionResult> Index()
        {
            var friends = await friendAppService.GetFriends(new GetFriendArg());

            return View(friends);
        }

        // GET: Friend/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Friend/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Friend/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("NickName,RelationType,Description")] Friend friend)
        {
            try
            {
                await friendAppService.Create(friend);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Friend/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Friend/Edit/5
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

        // GET: Friend/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Friend/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}