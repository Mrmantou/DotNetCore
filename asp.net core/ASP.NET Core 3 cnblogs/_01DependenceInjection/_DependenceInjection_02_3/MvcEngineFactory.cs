using System;
using System.Collections.Generic;
using System.Text;

namespace _DependenceInjection_02_3
{
    public class MvcEngineFactory : IMvcEngineFactory
    {
        public virtual IControllerActivator GetControllerActivator()
        {
            throw new NotImplementedException();
        }

        public virtual IControllerExecutor GetControllerExecutor()
        {
            throw new NotImplementedException();
        }

        public virtual IViewRenderer GetViewRender()
        {
            throw new NotImplementedException();
        }

        public virtual IWebListener GetWebListener()
        {
            throw new NotImplementedException();
        }
    }
}
