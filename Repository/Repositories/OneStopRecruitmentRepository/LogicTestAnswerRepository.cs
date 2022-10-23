using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface ILogicTestAnswerRepository : IRepository<LogicTestAnswerDTO>
    {
        LogicTestAnswerDTO GetCandidateLogicTestAnswer(Guid IDLogicTestQuestion, Guid IDCandidate, int IDPeriod);
        IEnumerable<LogicTestAnswerDTO> GetCandidateLogicTestScoreByCandidateIdList(List<Guid> IDCandidateList, int IDPeriod);
        IEnumerable<LogicTestAnswerDTO> GetCandidateLogicTestAnswerList(Guid IDCandidate, int IDPeriod);
    }

    public class LogicTestAnswerRepository : BaseRepository<LogicTestAnswerDTO>, ILogicTestAnswerRepository
    {
        public LogicTestAnswerRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public LogicTestAnswerDTO GetCandidateLogicTestAnswer(Guid IDLogicTestQuestion, Guid IDCandidate, int IDPeriod)
        {
            return Context.logicTestAnswerDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDCandidate.Equals(IDCandidate) && x.IDPeriod == IDPeriod &&
                            x.IDLogicTestQuestion.Equals(IDLogicTestQuestion)
                ).FirstOrDefault();
        }

        public IEnumerable<LogicTestAnswerDTO> GetCandidateLogicTestScoreByCandidateIdList(List<Guid> IDCandidateList, int IDPeriod)
        {
            return Context.logicTestAnswerDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            IDCandidateList.Contains(x.IDCandidate) && 
                            x.IDPeriod == IDPeriod
                );
        }

        public IEnumerable<LogicTestAnswerDTO> GetCandidateLogicTestAnswerList(Guid IDCandidate, int IDPeriod)
        {
            return Context.logicTestAnswerDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDCandidate.Equals(IDCandidate) &&
                            x.IDPeriod == IDPeriod
                );
        }
    }
}
