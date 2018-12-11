using Albert.Demo.Domain.UrlNavs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albert.Demo.EntityFramework.Sqlite.Seed.UrlNavs
{
    class InitialUrlNav : ISeedInitial
    {
        public void Create(DemoContext context)
        {
            if (!context.Set<UrlNav>().Any())
            {
                context.Set<UrlNav>().Add(new UrlNav
                {
                    Id = Guid.NewGuid(),
                    Title = "My HomePage",
                    Classify = "home page",
                    Url = "http://106.13.41.218",
                    Description = "My homepage, my style"
                });
                context.SaveChanges();
            }
        }
    }
}
