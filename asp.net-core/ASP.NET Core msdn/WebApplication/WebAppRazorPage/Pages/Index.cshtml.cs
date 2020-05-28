using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppRazorPage.DependencyInjection;
using WebAppRazorPage.Lifetime;

namespace WebAppRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private IMyDependency myDependency;
        public OperationService OperationService { get; }
        public IOperationTransient TransientOperation { get; }
        public IOperationScoped ScopedOperation { get; }
        public IOperationSingleton SingletonOperation { get; }
        public IOperationSingletonInstance SingletonInstanceOperation { get; }

        public IndexModel(
            IMyDependency myDependency,
            OperationService operationService,
            IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation,
            IOperationSingletonInstance singletonInstanceOperation)
        {
            this.myDependency = myDependency;
            OperationService = operationService;
            TransientOperation = transientOperation;
            ScopedOperation = scopedOperation;
            SingletonOperation = singletonOperation;
            SingletonInstanceOperation = singletonInstanceOperation;
        }
        
        public void OnGet()
        {

        }

        public async Task OnGetAsync()
        {
            await myDependency.WriteMessage("IndexModel.OnGetAsync created this message.");
        }
    }
}
