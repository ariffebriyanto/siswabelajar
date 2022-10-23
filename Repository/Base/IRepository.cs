using Model.DTO.Base;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Base
{
    public interface IRepository<TEntity> : IUnitOfWorkRepository where TEntity : BaseModel
    {
        IQueryable<TEntity> FindAll();
        TEntity Insert(TEntity entity);
        IEnumerable<TEntity> InsertMultiple(List<TEntity> entities);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
