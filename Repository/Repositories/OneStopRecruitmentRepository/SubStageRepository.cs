using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface ISubStageRepository : IRepository<SubStageDTO>
    {
        SubStageDTO GetSubStageByID(int IDSubStage);
        IEnumerable<SubStageDTO> GetSubStageByStageID(int IDStage);
        IEnumerable<SubStageDTO> GetAllSubStage();
        IEnumerable<SubStageDTO> GetSubStagesByIDSubStageList(List<int> IDSubStageList);
    }

    public class SubStageRepository : BaseRepository<SubStageDTO>, ISubStageRepository
    {
        public SubStageRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public SubStageDTO GetSubStageByID(int IDSubStage)
        {
            return Context.subStageDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) && 
                            x.IDSubStage == IDSubStage
                ).FirstOrDefault();
        }

        public IEnumerable<SubStageDTO> GetSubStageByStageID(int IDStage)
        {
            return Context.subStageDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDStage == IDStage
                );
        }

        public IEnumerable<SubStageDTO> GetAllSubStage()
        {
            return Context.subStageDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive)
                );
        }

        public IEnumerable<SubStageDTO> GetSubStagesByIDSubStageList(List<int> IDSubStageList)
        {
            return Context.subStageDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            IDSubStageList.Contains(x.IDSubStage)
                );
        }
    }
}
