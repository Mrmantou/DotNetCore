using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public interface IValueProvider
    {
        bool TryGetValues(string name, out string[] values);
        bool ContainsPrefix(string prefix);
    }
}
