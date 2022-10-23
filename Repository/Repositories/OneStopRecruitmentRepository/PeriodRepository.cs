using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface IPeriodRepository : IRepository<PeriodDTO>
    {
        PeriodDTO GetPeriodByID(int IDPeriod);
        PeriodDTO GetActivePeriod();
        IEnumerable<PeriodDTO> GetAllPeriod();
        IEnumerable<PeriodDTO> GetPeriodsByIDPeriodList(List<int> IDPeriodList);
    }

    public class PeriodRepository : BaseRepository<PeriodDTO>, IPeriodRepository
    {
        public PeriodRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            
        }

        public PeriodDTO GetPeriodByID(int IDPeriod)
        {
            return Context.periodDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            x.IDPeriod == IDPeriod
                ).FirstOrDefault();
        }

        public PeriodDTO GetActivePeriod()
        {
            return Context.periodDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            x.IsActive == ConstantConstraint.ACTIVE_PERIOD
                ).FirstOrDefault();
        }

        public IEnumerable<PeriodDTO> GetAllPeriod()
        {
            return Context.periodDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Delete)
                );
        }

        public IEnumerable<PeriodDTO> GetPeriodsByIDPeriodList(List<int> IDPeriodList)
        {
            return Context.periodDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            IDPeriodList.Contains(x.IDPeriod)
                );
        }
    }
}
