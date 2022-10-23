using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface ILogicTestQuestionTypeRepository : IRepository<LogicTestQuestionTypeDTO>
    {
        LogicTestQuestionTypeDTO SearchQuestionType(string QuestionType);
        LogicTestQuestionTypeDTO GetQuestionTypeByID(int IDQuestionType);
    }

    public class LogicTestQuestionTypeRepository : BaseRepository<LogicTestQuestionTypeDTO>, ILogicTestQuestionTypeRepository
    {
        public LogicTestQuestionTypeRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public LogicTestQuestionTypeDTO SearchQuestionType(string QuestionType)
        {
            return Context.Set<LogicTestQuestionTypeDTO>()
                .Where(x => x.LogicTestQuestionType.Trim().ToLower().Equals(QuestionType.Trim().ToLower())
                        && !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) && !x.StsRc.Equals(BaseConstraint.StsRc.Delete))
                .FirstOrDefault();
        }

        public LogicTestQuestionTypeDTO GetQuestionTypeByID(int IDQuestionType)
        {
            return Context.Set<LogicTestQuestionTypeDTO>()
                .Where(x => x.IDLogicTestQuestionType == IDQuestionType && x.StsRc != 'I' && x.StsRc != 'D')
                .FirstOrDefault();
        }
    }
}
