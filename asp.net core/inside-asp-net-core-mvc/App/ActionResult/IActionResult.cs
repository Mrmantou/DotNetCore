using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public interface IActionResult
    {
        Task ExecuteResultAsync(ActionContext context);
    }
}
