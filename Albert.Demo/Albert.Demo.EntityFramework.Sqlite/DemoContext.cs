using Albert.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albert.Demo.EntityFramework.Sqlite
{
    public class DemoContext : AlbertDbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options, Assembly.GetExecutingAssembly())
        {
        }
    }
}
