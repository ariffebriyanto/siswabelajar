using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface IMinimumScoreRepository : IRepository<MinimumScoreDTO>
    {
        MinimumScoreDTO GetMinimumScoreByID(int IDMinimumScore);
        MinimumScoreDTO CheckMinimumScore(int IDPeriod, int IDStage, int IDSubStage);
        IEnumerable<MinimumScoreDTO> CheckMinimumScoreBySubStageList(int IDPeriod, int IDStage, List<int> IDSubStageList);
    }

    public class MinimumScoreRepository : BaseRepository<MinimumScoreDTO>, IMinimumScoreRepository
    {
        public MinimumScoreRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public MinimumScoreDTO GetMinimumScoreByID(int IDMinimumScore)
        {
            return Context.minimumScoreDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDMinimumScore == IDMinimumScore
                ).FirstOrDefault();
        }

        public MinimumScoreDTO CheckMinimumScore(int IDPeriod, int IDStage, int IDSubStage)
        {
            return Context.minimumScoreDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDPeriod == IDPeriod &&
                            x.IDStage == IDStage &&
                            x.IDSubStage == IDSubStage
                ).FirstOrDefault();
        }

        public IEnumerable<MinimumScoreDTO> CheckMinimumScoreBySubStageList(int IDPeriod, int IDStage, List<int> IDSubStageList)
        {
            return Context.minimumScoreDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDPeriod == IDPeriod &&
                            x.IDStage == IDStage &&
                            IDSubStageList.Contains(x.IDSubStage)
                );
        }
    }
}
