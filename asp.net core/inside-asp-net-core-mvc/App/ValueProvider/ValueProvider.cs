using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class ValueProvider : IValueProvider
    {
        private readonly NameValueCollection values;

        public static ValueProvider Empty = new ValueProvider(new NameValueCollection());

        public ValueProvider(NameValueCollection values) => this.values = new NameValueCollection(StringComparer.OrdinalIgnoreCase) { values };

        public ValueProvider(IEnumerable<KeyValuePair<string, StringValues>> values)
        {
            this.values = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
            foreach (var keyValue in values)
            {
                foreach (var value in keyValue.Value)
                {
                    this.values.Add(keyValue.Key.Replace("-", ""), value);
                }
            }
        }

        public bool ContainsPrefix(string prefix)
        {
            foreach (string key in values.Keys)
            {
                if (key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public bool TryGetValues(string name, out string[] values)
        {
            values = this.values.GetValues(name);
            return values?.Any() == true;
        }
    }
}
