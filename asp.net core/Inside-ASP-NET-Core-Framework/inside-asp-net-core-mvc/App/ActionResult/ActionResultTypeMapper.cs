using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class ActionResultTypeMapper : IActionResultTypeMapper
    {
        public IActionResult Convert(object value, Type returnType) => new ContentResult(value.ToString(), "text/plain");
    }
}
