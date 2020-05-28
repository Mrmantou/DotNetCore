﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class HttpHeaderValueProviderFactory : IValueProviderFactory
    {
        public IValueProvider CreateValueProvider(ActionContext actionContext) => new ValueProvider(actionContext.HttpContext.Request.Headers);
    }
}
