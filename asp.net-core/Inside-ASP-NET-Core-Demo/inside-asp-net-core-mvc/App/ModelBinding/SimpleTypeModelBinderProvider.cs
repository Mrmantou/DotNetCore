using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class SimpleTypeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelMetadata metadata) => metadata.CanConvertFromString ? new SimpleTypeModelBinder() : null;
    }
}
