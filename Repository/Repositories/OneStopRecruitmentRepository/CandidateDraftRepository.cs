using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface ICandidateDraftRepository : IRepository<CandidateDraftDTO>
    {
        IEnumerable<CandidateDraftDTO> GetCandidateDraftByPeriodStagePosition(int IDPeriod, int IDStage, string IDPosition);
    }
    public class CandidateDraftRepository : BaseRepository<CandidateDraftDTO>, ICandidateDraftRepository
    {
        public CandidateDraftRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public IEnumerable<CandidateDraftDTO> GetCandidateDraftByPeriodStagePosition(int IDPeriod, int IDStage, string IDPosition)
        {
            return Context.candidateDraftDTOs
                .Where(x => x.IDPeriod == IDPeriod &&
                            x.IDStage == IDStage &&
                            x.IDPosition.Equals(IDPosition) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive));
        }
    }
}
