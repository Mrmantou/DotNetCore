using Albert.Demo.EntityFramework.Sqlite.Seed.Friends;
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
        }
    }
}
