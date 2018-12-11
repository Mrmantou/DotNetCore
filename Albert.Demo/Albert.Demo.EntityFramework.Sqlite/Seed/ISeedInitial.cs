using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.Demo.EntityFramework.Sqlite.Seed
{
    interface ISeedInitial
    {
        void Create(DemoContext context);
    }
}
