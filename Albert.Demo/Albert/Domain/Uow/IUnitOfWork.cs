using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Albert.Domain.Uow
{
    public interface IUnitOfWork
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
