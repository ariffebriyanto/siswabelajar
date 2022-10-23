using Microsoft.EntityFrameworkCore;

namespace Repository.Context
{
    public interface IDbContextFactory
    {
        DbContext GetContext();
        DbContext GetOracleContext();
    }

    public class DbContextFactory : IDbContextFactory
    {
        private readonly DbContextOptions<OneStopRecruitmentDbContext> options;
        private readonly DbContextOptions<OracleDbContext> optionsOracle;
        private readonly IApplicationSessionDataAccessor applicationSessionDataAccessor;
        public DbContextFactory(
            DbContextOptions<OneStopRecruitmentDbContext> options,
            DbContextOptions<OracleDbContext> optionsOracle,
            IApplicationSessionDataAccessor applicationSessionDataAccessor
        )
        {
            this.options = options;
            this.optionsOracle = optionsOracle;
            this.applicationSessionDataAccessor = applicationSessionDataAccessor;
        }

        public DbContext GetContext()
        {
            DbContext ctx = new OneStopRecruitmentDbContext(options, applicationSessionDataAccessor);
            ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return ctx;
        }

        public DbContext GetOracleContext()
        {
            DbContext ctx = new OracleDbContext(optionsOracle, applicationSessionDataAccessor);
            ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return ctx;
        }
    }
}