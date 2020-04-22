using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    public class DefaultActionDescriptorCollectionProvider : IActionDescriptorCollectionProvider
    {
        private readonly Lazy<IReadOnlyList<ActionDescriptor>> accessor;

        public IReadOnlyList<ActionDescriptor> ActionDescriptors => accessor.Value;

        public DefaultActionDescriptorCollectionProvider(IEnumerable<IActionDescriptorProvider> providers) => accessor = new Lazy<IReadOnlyList<ActionDescriptor>>(() => providers.SelectMany(p => p.ActionDescriptors).ToList());
    }
}
