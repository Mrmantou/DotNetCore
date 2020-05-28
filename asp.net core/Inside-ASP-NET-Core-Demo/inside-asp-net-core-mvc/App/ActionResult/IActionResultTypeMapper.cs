using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public interface IActionResultTypeMapper
    {
        IActionResult Convert(object value, Type returnType);
    }
}
