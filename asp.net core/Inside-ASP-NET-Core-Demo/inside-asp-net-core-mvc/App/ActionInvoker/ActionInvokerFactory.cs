using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class ActionInvokerFactory : IActionInvokerFactory
    {
        public IActionInvoker CreateInvoker(ActionContext actionContext) => new ControllerActionInvoker(actionContext);
    }
}
