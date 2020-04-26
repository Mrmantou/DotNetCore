using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace App
{
    public class ComplexTypeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelMetadata metadata)
        {
            if (metadata.CanConvertFromString)
            {
                return null;
            }

            return metadata.Parameter?.GetCustomAttribute<FromBodyAttribute>() == null
                ? new ComplexTypeModelBinder()
                : null;
        }
    }
}
