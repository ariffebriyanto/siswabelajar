using Microsoft.EntityFrameworkCore;
using Model.DTO.Base;
using Repository.Context;
using System.Collections.Generic;
using System.Linq;
using static Model.DBConstraint.BaseConstraint;

namespace Repository.Base
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
    {
        protected OneStopRecruitmentDbContext Context;
        private OneStopRecruitmentDbContext PrevDbContext;

        public BaseRepository(IDbContextFactory dbContextFactory)
        {
            Context = (OneStopRecruitmentDbContext)dbContextFactory.GetContext();
        }

        public void UseContext(DbContext context)
        {
            PrevDbContext = this.Context;
            this.Context = (OneStopRecruitmentDbContext)context;
        }
        public void RevertToPreviousDbContext()
        {
            this.Context = PrevDbContext;
            PrevDbContext = null;
        }
        public IQueryable<TEntity> FindAll()
        {
            IQueryable<TEntity> entities = Context.Set<TEntity>().Where(x => !x.StsRc.Equals(StsRc.Delete) && !x.StsRc.Equals(StsRc.Inactive));
            return entities;
        }
        public TEntity Insert(TEntity entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public IEnumerable<TEntity> InsertMultiple(List<TEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                Context.Add(entities[i]);
            }

            Context.SaveChanges();
            return entities;
        }
        public TEntity Update(TEntity entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public void Delete(TEntity entity)
        {
            if (entity != null)
            {
                Context.Remove(entity);
                Context.SaveDeletion();
            }
        }
    }
}