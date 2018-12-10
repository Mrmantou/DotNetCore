using Albert.Demo.Application.Friends.Dtos;
using Albert.Demo.Domain.Friends;
using Albert.Domain.Repositories;
using Albert.Domain.Uow;
using Albert.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albert.Demo.Application.Friends
{
    public class FriendAppService : IFriendAppService
    {
        private readonly IRepository<Friend> repository;
        private readonly IUnitOfWork unitOfWork;
        public FriendAppService(IRepository<Friend> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Friend>> GetFriends(GetFriendArg input)
        {
            return await repository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.NickName), f => f.NickName == input.NickName)
                .ToListAsync();
        }

        public async Task Create(Friend friend)
        {
            await repository.InsertAsync(friend);
            unitOfWork.SaveChanges();
        }
    }
}
