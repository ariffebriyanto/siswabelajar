using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface ICandidateScoreRepository : IRepository<CandidateScoreDTO>
    {
        IEnumerable<CandidateScoreDTO> GetCandidateScoreListByQuestionID(List<Guid> QuestionList, Guid IDCandidate);
        IEnumerable<CandidateScoreDTO> GetCandidateScoreListByQuestionIDAndCandidateIdList(List<ScoringComponentDTO> QuestionList, List<Guid> IDCandidateList);
        IEnumerable<CandidateScoreDTO> GetCandidateScoreListByQuestionIDList(List<Guid> IDQuestionList);
    }

    public class CandidateScoreRepository : BaseRepository<CandidateScoreDTO>, ICandidateScoreRepository
    {
        public CandidateScoreRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public IEnumerable<CandidateScoreDTO> GetCandidateScoreListByQuestionID(List<Guid> QuestionList, Guid IDCandidate)
        {            
            return Context.candidateScoreDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            QuestionList.Contains(x.IDScoringComponent) &&
                            x.IDCandidate.Equals(IDCandidate)
                );
        }

        public IEnumerable<CandidateScoreDTO> GetCandidateScoreListByQuestionIDAndCandidateIdList(List<ScoringComponentDTO> QuestionList, List<Guid> IDCandidateList)
        {
            HashSet<Guid> QuestionID = new HashSet<Guid>(QuestionList.Select(x => x.IDScoringComponent));
            return Context.candidateScoreDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            QuestionID.Contains(x.IDScoringComponent) &&
                            IDCandidateList.Contains(x.IDCandidate)
                );
        }

        public IEnumerable<CandidateScoreDTO> GetCandidateScoreListByQuestionIDList(List<Guid> IDQuestionList)
        {
            return Context.candidateScoreDTOs
                    .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                                !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                                IDQuestionList.Contains(x.IDScoringComponent)
                    );
        }
    }
}
