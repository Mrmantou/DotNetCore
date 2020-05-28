using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace _Configuration_09
{
    public class ApplicationSettingsContext : DbContext
    {
        public ApplicationSettingsContext(DbContextOptions options) : base(options) { }

        public DbSet<ApplicationSetting> Settings { get; set; }
    }
}
