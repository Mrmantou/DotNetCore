using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class ModelBindingContext
    {
        public ActionContext ActionContext { get; }
        public string ModelName { get; }
        public ModelMetadata ModelMetadata { get; }
        public IValueProvider ValueProvider { get; }

        public object Model { get; private set; }
        public bool IsModelSet { get; private set; }

        public ModelBindingContext(ActionContext actionContext, string modelName, ModelMetadata modelMetadata, IValueProvider valueProvider)
        {
            ActionContext = actionContext;
            ModelName = modelName;
            ModelMetadata = modelMetadata;
            ValueProvider = valueProvider;
        }

        public void Bind(object model)
        {
            Model = model;
            IsModelSet = true;
        }
    }
}
