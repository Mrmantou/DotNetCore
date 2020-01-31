using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _DependenceInjection_02_1
{
    public class FoobarMvcEngine : MvcEngine
    {
        protected override Task<Controller> CreateControllerAsync(Request request)
        {
            //......
            return base.CreateControllerAsync(request);
        }
    }
}
