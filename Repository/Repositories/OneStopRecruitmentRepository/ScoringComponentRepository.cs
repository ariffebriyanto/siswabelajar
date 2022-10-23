using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface IScoringComponentRepository : IRepository<ScoringComponentDTO>
    {
        ScoringComponentDTO GetScoringComponentByID(Guid IDScoringComponent);
        IEnumerable<ScoringComponentDTO> GetAllScoringComponent();
        IEnumerable<ScoringComponentDTO> GetScoringComponentListByStage(int IDPeriod, int IDStage, int IDSubStage, string IDPosition);
        IEnumerable<ScoringComponentDTO> GetScoringComponentListByStageAndSubstageIdList(int IDPeriod, int IDStage, List<int> IDSubStageList, string IDPosition);
        IEnumerable<ScoringComponentDTO> GetScoringComponentListByPeriodStageSubstage(int IDPeriod, int IDStage, int IDSubStage);
    }
    public class ScoringComponentRepository : BaseRepository<ScoringComponentDTO>, IScoringComponentRepository
    {
        public ScoringComponentRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public ScoringComponentDTO GetScoringComponentByID(Guid IDScoringComponent)
        {
            return Context.scoringComponentDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) && 
                            !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            x.IDScoringComponent.Equals(IDScoringComponent)
                ).FirstOrDefault();
        }

        public IEnumerable<ScoringComponentDTO> GetAllScoringComponent()
        {
            return Context.scoringComponentDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) && 
                            !x.StsRc.Equals(BaseConstraint.StsRc.Delete)
            );
        }

        public IEnumerable<ScoringComponentDTO> GetScoringComponentListByStage(int IDPeriod, int IDStage, int IDSubStage, string IDPosition)
        {
            return Context.scoringComponentDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            x.IDPeriod == IDPeriod && 
                            x.IDStage == IDStage && 
                            x.IDSubStage == IDSubStage &&
                            x.IDPosition.Contains(IDPosition)
                );
        }

        public IEnumerable<ScoringComponentDTO> GetScoringComponentListByStageAndSubstageIdList(int IDPeriod, int IDStage, List<int> IDSubStageList, string IDPosition)
        {
            return Context.scoringComponentDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            x.IDPeriod == IDPeriod &&
                            x.IDStage == IDStage &&
                            IDSubStageList.Contains(x.IDSubStage) &&
                            x.IDPosition.Contains(IDPosition)
                );
        }

        public IEnumerable<ScoringComponentDTO> GetScoringComponentListByPeriodStageSubstage(int IDPeriod, int IDStage, int IDSubStage)
        {
            return Context.scoringComponentDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            x.IDPeriod == IDPeriod &&
                            x.IDStage == IDStage &&
                            x.IDSubStage == IDSubStage
                );
        }
    }
}