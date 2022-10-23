using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface IBlastEmailRepository : IRepository<BlastEmailDTO>
    {
        IEnumerable<BlastEmailDTO> GetByIDPeriod(int IDPeriod);
    }
    public class BlastEmailRepository : BaseRepository<BlastEmailDTO>, IBlastEmailRepository
    {
        public BlastEmailRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public IEnumerable<BlastEmailDTO> GetByIDPeriod(int IDPeriod)
        {
            return Context.blastEmailDTOs.Where(x =>
                    !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                    !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                    x.IDPeriod == IDPeriod
                );
        }
    }
}
