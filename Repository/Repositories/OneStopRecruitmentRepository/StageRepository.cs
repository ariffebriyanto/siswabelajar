using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface IStageRepository : IRepository<StageDTO>
    {
        StageDTO GetStageById(int IDStage);
        IEnumerable<StageDTO> GetAllStage();
        IEnumerable<StageDTO> GetStagesByIDStageList(List<int> IDStageList);
        StageDTO GetLastStage();
    }

    public class StageRepository : BaseRepository<StageDTO>, IStageRepository
    {
        public StageRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public StageDTO GetStageById(int IDStage)
        {
            return Context.stageDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDStage == IDStage)
                .FirstOrDefault();
        }

        public IEnumerable<StageDTO> GetAllStage()
        {
            return Context.stageDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive)
                );
        }

        public IEnumerable<StageDTO> GetStagesByIDStageList(List<int> IDStageList)
        {
            return Context.stageDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            IDStageList.Contains(x.IDStage)
                );
        }

        public StageDTO GetLastStage()
        {
            return Context.stageDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive)
                ).OrderByDescending(x => x.StageLevel).FirstOrDefault();
        }
    }
}
