using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.Core.Entities
{
    public abstract class Entity : Entity<int>, IEntity
    {
    }

    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }

        public override int GetHashCode()
        {
            if (Id == null)
            {
                return 0;
            }

            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }
    }
}
