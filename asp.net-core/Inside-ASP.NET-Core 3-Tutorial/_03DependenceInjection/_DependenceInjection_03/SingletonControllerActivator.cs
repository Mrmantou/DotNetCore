﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _DependenceInjection_03
{
    public class SingletonControllerActivator : IControllerActivator
    {
        public Task<Controller> CreateControllerAsync(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public Task ReleaseAsync(Controller controller) => Task.CompletedTask;
    }
}
