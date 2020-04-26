using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public abstract class Controller
    {
        public ActionContext ActionContext { get; internal set; }
    }
}
