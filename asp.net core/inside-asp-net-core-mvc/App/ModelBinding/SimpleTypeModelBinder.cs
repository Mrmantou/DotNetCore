using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class SimpleTypeModelBinder : IModelBinder
    {
        public Task BindAsync(ModelBindingContext context)
        {
            if (context.ValueProvider.TryGetValues(context.ModelName, out var values))
            {
                var model = Convert.ChangeType(values.Last(), context.ModelMetadata.ModelType);
                context.Bind(model);
            }

            return Task.CompletedTask;
        }
    }
}
