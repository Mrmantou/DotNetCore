using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppRazorPage.Lifetime
{
    public interface IOperation
    {
        Guid OperationId { get; }
    }
}
