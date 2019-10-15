﻿using Albert.Demo.Application.Friends;
using Albert.Demo.Application.Friends.Dtos;
using Albert.Demo.Domain.Friends;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Albert.Demo.Web.Controllers
{
    public class FriendController : Controller
    {
        private readonly IFriendAppService friendAppService;

        public FriendController(IFriendAppService friendAppService)
        {
            this.friendAppService = friendAppService;
        }

        // GET: Friend
        public async Task<ActionResult> Index(GetFriendArg input)
        {
            var friends = await friendAppService.GetFriends(input);

            return View(friends);
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
    }
}