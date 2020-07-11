using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Albert.Demo.Application.Friends;
using Albert.Demo.Application.Friends.Dtos;
using Albert.Demo.Domain.Friends;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Albert.Demo.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendAppService friendAppService;

        public FriendController(IFriendAppService friendAppService)
        {
            this.friendAppService = friendAppService;
        }

        [HttpGet]
        public async Task<List<Friend>> Get(string nickName)
        {
            return await friendAppService.GetFriends(new GetFriendArg { NickName = nickName });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Friend friend)
        {
            await friendAppService.Create(friend);

            return Ok();
        }
    }
}
