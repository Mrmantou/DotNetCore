using Albert.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Albert.Demo.EntityFramework.Sqlite
{
    public class DemoContext : AlbertDbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options, Assembly.GetExecutingAssembly())
        {
        }
    }
}
