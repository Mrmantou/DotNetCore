using Albert.Demo.Domain.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albert.Demo.EntityFramework.Sqlite.Seed.Friends
{
    class InitialFriend : ISeedInitial
    {
        public void Create(DemoContext context)
        {
            if (!context.Set<Friend>().Any())
            {
                context.Set<Friend>().Add(new Friend
                {
                    NickName = "mantou",
                    RelationType = RelationType.Others,
                    Description = "Myself"
                });
                context.SaveChanges();
            }
        }
    }
}
