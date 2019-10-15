using Microsoft.EntityFrameworkCore;

namespace Albert.EntityFrameworkCore.Repositories
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}
