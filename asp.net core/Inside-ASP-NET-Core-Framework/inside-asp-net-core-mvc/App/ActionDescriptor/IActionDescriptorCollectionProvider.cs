using System.Collections.Generic;

namespace App
{
    public interface IActionDescriptorCollectionProvider
    {
        IReadOnlyList<ActionDescriptor> ActionDescriptors { get; }
    }
}
