using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.Domain.Entities
{
    public interface IAggregateRoot : IEntity<int>
    {

    }
    
    public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>
    {

    }
}
