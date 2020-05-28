using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace App
{
    public class BodyModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelMetadata metadata)
        {
            return metadata.Parameter?.GetCustomAttribute<FromBodyAttribute>() == null
                ? null
                : new BodyModelBinder();
        }
    }
}
