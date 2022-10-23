using Microsoft.EntityFrameworkCore;
using Model.DTO.OracleDTO;

namespace Repository.Context
{
    public class OracleDbContext : DbContext
	{
		private readonly IApplicationSessionDataAccessor applicationSessionDataAccessor;
		public OracleDbContext(DbContextOptions<OracleDbContext> options, IApplicationSessionDataAccessor applicationSessionDataAccessor) : base(options)
		{
			this.applicationSessionDataAccessor = applicationSessionDataAccessor;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PS_N_PERSONCAR_TBLDTO>()
				.HasKey(c => new { c.EMPLID, c.EXTERNAL_SYSTEM_ID, c.ACAD_CAREER });
		}
	}
}