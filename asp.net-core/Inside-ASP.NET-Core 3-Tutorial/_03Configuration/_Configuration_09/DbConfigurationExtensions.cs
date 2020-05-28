using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace _Configuration_09
{
    public static class DbConfigurationExtensions
    {
        public static IConfigurationBuilder AddDataBase(this IConfigurationBuilder builder, string connectionStringName, IDictionary<string, string> initialSettings = null)
        {
            var connectionString = builder.Build().GetConnectionString(connectionStringName);
            var source = new DbConfigurationSource(optionBuilder => optionBuilder.UseSqlServer(connectionString), initialSettings);

            builder.Add(source);
            return builder;
        }
    }
}
