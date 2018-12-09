using Albert.Demo.Domain.Friends;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Albert.Demo.EntityFramework.Sqlite.Seed.Friends
{
    class InitialFriend
    {
        public void Create(DemoContext context)
        {
           if(! context.Set<Friend>().Any())
            {
                context.Set<Friend>().Add(new Friend { NickName = "mantou", RelationType = RelationType.Others, Description = "Myself" });
                context.SaveChanges();
            }
        }
    }
}
