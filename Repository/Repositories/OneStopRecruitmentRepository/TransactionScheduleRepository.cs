using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface ITransactionScheduleRepository : IRepository<TransactionScheduleDTO>
    {
        IEnumerable<TransactionScheduleDTO> GetByIDSchedule(Guid IDSchedule);
        IEnumerable<TransactionScheduleDTO> GetByIDUser(Guid IDUser);
        IEnumerable<TransactionScheduleDTO> GetByIDScheduleList(List<Guid> IDScheduleList);
        TransactionScheduleDTO GetByScheduleAndUser(Guid IDSchedule, Guid IDUser);
        int GetScheduleStudentCountById(Guid IDSchedule);
    }
    public class TransactionScheduleRepository : BaseRepository<TransactionScheduleDTO>, ITransactionScheduleRepository
    {
        public TransactionScheduleRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public IEnumerable<TransactionScheduleDTO> GetByIDSchedule(Guid IDSchedule)
        {
            return Context.transactionScheduleDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDSchedule.Equals(IDSchedule));            
        }

        public IEnumerable<TransactionScheduleDTO> GetByIDUser(Guid IDUser)
        {
            return Context.transactionScheduleDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDUser.Equals(IDUser));
        }

        public IEnumerable<TransactionScheduleDTO> GetByIDScheduleList(List<Guid> IDScheduleList)
        {
            return Context.transactionScheduleDTOs
                   .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                               !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                               IDScheduleList.Contains(x.IDSchedule));
        }

        public TransactionScheduleDTO GetByScheduleAndUser(Guid IDSchedule, Guid IDUser)
        {
            return Context.transactionScheduleDTOs
                   .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                               !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                               x.IDSchedule.Equals(IDSchedule) &&
                               x.IDUser.Equals(IDUser)).FirstOrDefault();
        }

        public int GetScheduleStudentCountById(Guid IDSchedule)
        {
            return Context.transactionScheduleDTOs
                    .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                                !x.StsRc.Equals(BaseConstraint.StsRc.Inactive))
                    .Where(x => x.IDSchedule.Equals(IDSchedule))
                    .Select(x => x.IDUser).Distinct().Count();
        }
    }
}
