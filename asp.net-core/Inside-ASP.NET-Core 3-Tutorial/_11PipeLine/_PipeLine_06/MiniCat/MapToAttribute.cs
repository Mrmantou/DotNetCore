﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _PipeLine_06.MiniCat
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class MapToAttribute : Attribute
    {
        public Type ServiceType { get; }
        public LifeTime LifeTime { get; }

        public MapToAttribute(Type serviceType, LifeTime lifeTime)
        {
            ServiceType = serviceType;
            LifeTime = lifeTime;
        }
    }
}
