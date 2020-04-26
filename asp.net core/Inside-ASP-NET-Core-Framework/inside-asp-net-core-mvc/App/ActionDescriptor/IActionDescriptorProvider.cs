using System.Collections.Generic;

namespace App
{
    public interface IActionDescriptorProvider
    {
        IEnumerable<ActionDescriptor> ActionDescriptors { get; }
    }
}
