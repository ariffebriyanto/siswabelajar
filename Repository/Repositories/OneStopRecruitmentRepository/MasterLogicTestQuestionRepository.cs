using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface IMasterLogicTestQuestionRepository : IRepository<MasterLogicTestQuestionDTO>
    {
        MasterLogicTestQuestionDTO GetQuestionByID(Guid IDLogicTestQuestion);
        IEnumerable<MasterLogicTestQuestionDTO> GetQuestionsByIDQuestionList(List<Guid> IDQuestionList);
    }

    public class MasterLogicTestQuestionRepository : BaseRepository<MasterLogicTestQuestionDTO>, IMasterLogicTestQuestionRepository
    {
        public MasterLogicTestQuestionRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public MasterLogicTestQuestionDTO GetQuestionByID(Guid IDLogicTestQuestion)
        {
            return Context.masterLogicTestQuestionDTOs.Where(x => x.IDLogicTestQuestion == IDLogicTestQuestion
                && !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) && !x.StsRc.Equals(BaseConstraint.StsRc.Delete))
                .FirstOrDefault();
        }

        public IEnumerable<MasterLogicTestQuestionDTO> GetQuestionsByIDQuestionList(List<Guid> IDQuestionList)
        {
            return Context.masterLogicTestQuestionDTOs.Where(x => IDQuestionList.Contains(x.IDLogicTestQuestion)
                && !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) && !x.StsRc.Equals(BaseConstraint.StsRc.Delete));
        }
    }
}
