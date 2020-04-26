using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public interface IModelBinder
    {
        public Task BindAsync(ModelBindingContext context);
    }
}
