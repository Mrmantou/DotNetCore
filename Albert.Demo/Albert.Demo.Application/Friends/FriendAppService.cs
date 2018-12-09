using System;
using System.Collections.Generic;
using System.Text;
using Albert.Demo.Application.Friends.Dtos;
using Albert.Demo.Domain.Friends;
using Albert.Domain.Repositories;
using Albert.Linq.Extensions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Albert.Demo.Application.Friends
{
    public class FriendAppService : IFriendAppService
    {
        private readonly IRepository<Friend> repository;

        public FriendAppService(IRepository<Friend> repository)
        {
            this.repository = repository;
        }

        public async Task<List<Friend>> GetFriends(GetFriendArg input)
        {
            return await repository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.NickName), f => f.NickName == input.NickName)
                .ToListAsync();
        }
    }
}
