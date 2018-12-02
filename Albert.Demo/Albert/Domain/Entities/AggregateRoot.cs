using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.Domain.Entities
{
    public class AggregateRoot : AggregateRoot<int>, IAggregateRoot
    {
    }


    public class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
    {

    }
}
