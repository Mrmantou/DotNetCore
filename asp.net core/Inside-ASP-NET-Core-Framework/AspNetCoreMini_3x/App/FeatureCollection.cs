using System;
using System.Collections.Generic;

namespace App
{
    public interface IFeatureCollection : IDictionary<Type, object> { }

    public class FeatureCollection : Dictionary<Type, object>, IFeatureCollection { }
}
