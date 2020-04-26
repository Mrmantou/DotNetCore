using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class ModelBinderFactory : IModelBinderFactory
    {
        private readonly IEnumerable<IModelBinderProvider> providers;

        public ModelBinderFactory(IEnumerable<IModelBinderProvider> providers) => this.providers = providers;

        public IModelBinder CreateBinder(ModelMetadata metadata)
        {
            foreach (var provider in providers)
            {
                var binder = provider.GetBinder(metadata);
                if (binder != null)
                {
                    return binder;
                }
            }

            return null;
        }
    }
}
