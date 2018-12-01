using Albert.DataModel;
using Albert.EntityFramework.Sqlite.Mapping;
using Albert.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albert.EntityFramework.Sqlite
{
    public class FriendContext : AlbertDbContext
    {
        public FriendContext(DbContextOptions<FriendContext> options) : base(options)
        {
        }
    }
}
