using Albert.Demo.EntityFramework.Sqlite.Seed.Friends;
using Albert.Demo.EntityFramework.Sqlite.Seed.UrlNavs;

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
