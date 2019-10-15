using System.Threading.Tasks;

namespace Albert.Domain.Uow
{
    public interface IUnitOfWork
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
