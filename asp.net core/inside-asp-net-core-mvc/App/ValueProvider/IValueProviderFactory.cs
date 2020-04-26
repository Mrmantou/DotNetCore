using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public interface IValueProviderFactory
    {
        IValueProvider CreateValueProvider(ActionContext actionContext);
    }
}
