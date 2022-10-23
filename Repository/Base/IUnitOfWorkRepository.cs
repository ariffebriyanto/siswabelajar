using Microsoft.EntityFrameworkCore;

namespace Repository.Base
{
    public interface IUnitOfWorkRepository
    {
        void UseContext(DbContext context);
        void RevertToPreviousDbContext();
    }
}