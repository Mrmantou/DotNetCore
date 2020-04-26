using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public interface IModelBinderFactory
    {
        IModelBinder CreateBinder(ModelMetadata metadata);
    }
}
