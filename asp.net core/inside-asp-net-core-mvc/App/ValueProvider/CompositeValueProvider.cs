using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class CompositeValueProvider : IValueProvider
    {
        private readonly IEnumerable<IValueProvider> providers;

        public CompositeValueProvider(IEnumerable<IValueProvider> providers) => this.providers = providers;

        public bool ContainsPrefix(string prefix) => providers.Any(i => i.ContainsPrefix(prefix));

        public bool TryGetValues(string name, out string[] values)
        {
            foreach (var provider in providers)
            {
                if (provider.TryGetValues(name, out values))
                {
                    return true;
                }
            }

            return (values = null) != null;
        }
    }
}
