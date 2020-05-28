using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _Configuration_09
{
    public class DbConfigurationProvider : ConfigurationProvider
    {
        private Action<DbContextOptionsBuilder> setup;
        private IDictionary<string, string> initialSettings;

        public DbConfigurationProvider(Action<DbContextOptionsBuilder> setup, IDictionary<string, string> initialSettings)
        {
            this.setup = setup;
            this.initialSettings = initialSettings ?? new Dictionary<string, string>();
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<ApplicationSettingsContext>();
            setup(builder);

            using (ApplicationSettingsContext context = new ApplicationSettingsContext(builder.Options))
            {
                context.Database.EnsureCreated();
                Data = context.Settings.Any() ? context.Settings.ToDictionary(i => i.Key, i => i.Value, StringComparer.OrdinalIgnoreCase) : Initialize(context);
            }
        }

        private IDictionary<string, string> Initialize(ApplicationSettingsContext context)
        {
            foreach (var item in initialSettings)
            {
                context.Settings.Add(new ApplicationSetting(item.Key, item.Value));
            }

            return initialSettings.ToDictionary(i => i.Key, i => i.Value, StringComparer.OrdinalIgnoreCase);
        }
    }
}
