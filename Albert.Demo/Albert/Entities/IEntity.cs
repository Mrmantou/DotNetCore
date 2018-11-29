using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.Core.Entities
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }

    public interface IEntity : IEntity<int>
    {

    }
}
