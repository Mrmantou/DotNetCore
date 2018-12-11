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