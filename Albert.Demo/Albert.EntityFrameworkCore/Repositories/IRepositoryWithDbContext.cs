using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.EntityFrameworkCore.Repositories
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}
