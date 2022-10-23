using Repository.Context;

namespace Repository.Base
{
    public class OracleBaseRepository<TEntity>
    {
        protected OracleDbContext Context;

        public OracleBaseRepository(IDbContextFactory dbContextFactory)
        {
            Context = (OracleDbContext) dbContextFactory.GetOracleContext();
        }
    }
}