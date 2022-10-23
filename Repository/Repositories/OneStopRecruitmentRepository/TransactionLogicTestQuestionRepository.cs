using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface ITransactionLogicTestQuestionRepository : IRepository<TransactionLogicTestQuestionDTO>
    {
        IEnumerable<TransactionLogicTestQuestionDTO> GetSelectedQuestionByPeriod(int IDPeriod);
        TransactionLogicTestQuestionDTO GetTransactionByID(Guid IDLogicTestQuestion, int IDPeriod);
        IEnumerable<TransactionLogicTestQuestionDTO> GetTransactionListByIDQuestion(Guid IDLogicTestQuestion);
    }

    public class TransactionLogicTestQuestionRepository : BaseRepository<TransactionLogicTestQuestionDTO>, ITransactionLogicTestQuestionRepository
    {
        public TransactionLogicTestQuestionRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
        
        public IEnumerable<TransactionLogicTestQuestionDTO> GetSelectedQuestionByPeriod(int IDPeriod)
        {
            return Context.transactionLogicTestQuestionDTOs
                .Where(x => x.IDPeriod == IDPeriod && !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) && !x.StsRc.Equals(BaseConstraint.StsRc.Delete));
        }

        public TransactionLogicTestQuestionDTO GetTransactionByID(Guid IDLogicTestQuestion, int IDPeriod)
        {
            return Context.transactionLogicTestQuestionDTOs.Where(x => x.IDLogicTestQuestion.Equals(IDLogicTestQuestion) &&
                            x.IDPeriod == IDPeriod && !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) && !x.StsRc.Equals(BaseConstraint.StsRc.Delete))
                .FirstOrDefault();
        }

        public IEnumerable<TransactionLogicTestQuestionDTO> GetTransactionListByIDQuestion(Guid IDLogicTestQuestion)
        {
            return Context.transactionLogicTestQuestionDTOs.Where(x => x.IDLogicTestQuestion.Equals(IDLogicTestQuestion) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) && !x.StsRc.Equals(BaseConstraint.StsRc.Delete));
        }
    }
}
