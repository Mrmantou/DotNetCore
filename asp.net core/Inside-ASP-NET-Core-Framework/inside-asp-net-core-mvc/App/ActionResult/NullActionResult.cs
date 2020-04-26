using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class NullActionResult : IActionResult
    {
        private NullActionResult() { }

        public static NullActionResult Instance { get; } = new NullActionResult();

        public Task ExecuteResultAsync(ActionContext context) => Task.CompletedTask;
    }
}
