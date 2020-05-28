using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace _Configuration_09
{
    public class DbConfigurationSource : IConfigurationSource
    {
        private Action<DbContextOptionsBuilder> setup;
        private IDictionary<string, string> initialSettings;

        public DbConfigurationSource(Action<DbContextOptionsBuilder> setup, IDictionary<string, string> initialSettings = null)
        {
            this.setup = setup;
            this.initialSettings = initialSettings;
        }


        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DbConfigurationProvider(setup, initialSettings);
        }
    }
}
