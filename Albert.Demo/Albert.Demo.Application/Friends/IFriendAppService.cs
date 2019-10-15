using Albert.Demo.Application.Friends.Dtos;
using Albert.Demo.Domain.Friends;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Albert.Demo.Application.Friends
{
    public interface IFriendAppService
    {
        Task<List<Friend>> GetFriends(GetFriendArg input);

        Task Create(Friend friend);
    }
}
