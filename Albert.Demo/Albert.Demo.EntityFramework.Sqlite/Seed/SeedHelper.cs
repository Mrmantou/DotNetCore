using Albert.Demo.EntityFramework.Sqlite.Seed.Friends;
using Albert.Demo.EntityFramework.Sqlite.Seed.UrlNavs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.Demo.EntityFramework.Sqlite.Seed
{
    public static class SeedHelper
    {
        public static void Create(DemoContext context)
        {
            new InitialFriend().Create(context);
            new InitialUrlNav().Create(context);
        }
    }
}
