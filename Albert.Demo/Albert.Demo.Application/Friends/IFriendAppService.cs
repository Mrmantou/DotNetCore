using Albert.Demo.Application.Friends.Dtos;
using Albert.Demo.Domain.Friends;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.Demo.Application.Friends
{
    public interface IFriendAppService
    {
        List<Friend> GetFriends(GetFriendArg input);
    }
}
