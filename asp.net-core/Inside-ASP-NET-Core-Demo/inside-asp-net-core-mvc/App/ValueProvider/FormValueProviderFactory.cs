using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class FormValueProviderFactory : IValueProviderFactory
    {
        public IValueProvider CreateValueProvider(ActionContext actionContext)
        {
            var contentType = actionContext.HttpContext.Request.GetTypedHeaders().ContentType;
            return contentType.MediaType.Equals("application/x-www-form-urlencoded")
                ? new ValueProvider(actionContext.HttpContext.Request.Form)
                : ValueProvider.Empty;
        }
    }
}
