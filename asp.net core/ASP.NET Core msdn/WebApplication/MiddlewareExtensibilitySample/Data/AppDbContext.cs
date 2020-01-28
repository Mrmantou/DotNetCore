using Microsoft.EntityFrameworkCore;
using MiddlewareExtensibilitySample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExtensibilitySample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Request> Requests { get; set; }
    }
}
