using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.AssignmentSubdomain.Staff;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface ISubmissionRepository : IRepository<SubmissionDTO>
    {
        SubmissionDTO GetLastSubmission(int IDAssignment, Guid IDUser);
        SubmissionDTO GetByID(Guid IDSubmission);
        IEnumerable<SubmissionDTO> GetSubmissionByIDAssignmentList(List<int> IDAssignmentList);
        IEnumerable<SubmissionDTO> GetSubmissionByIDAssignment(int IDAssignment);
        List<Submission> GetSubmissionsByIDAssignmentAndIDSubmission(int IDAssignment, Guid IDSchedule);
    }

    public class SubmissionRepository : BaseRepository<SubmissionDTO>, ISubmissionRepository
    {
        public SubmissionRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            
        }

        public SubmissionDTO GetLastSubmission(int IDAssignment, Guid IDUser)
        {
            return Context.submissionDTOs.Where(x => 
                    !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                    !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                    x.IDAssignment == IDAssignment && 
                    x.IDUser.Equals(IDUser)
                ).FirstOrDefault();
        }

        public SubmissionDTO GetByID(Guid IDSubmission)
        {
            return Context.submissionDTOs.Where(x =>
                    !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                    !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&                    
                    x.IDSubmission.Equals(IDSubmission)
                ).FirstOrDefault();
        }

        public IEnumerable<SubmissionDTO> GetSubmissionByIDAssignmentList(List<int> IDAssignmentList)
        {
            return Context.submissionDTOs.Where(x =>
                    !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                    !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                    IDAssignmentList.Contains(x.IDAssignment)
                );
        }

        public IEnumerable<SubmissionDTO> GetSubmissionByIDAssignment(int IDAssignment)
        {
            return Context.submissionDTOs.Where(x =>
                    !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                    !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                    x.IDAssignment == IDAssignment
                );
        }

        public List<Submission> GetSubmissionsByIDAssignmentAndIDSubmission(int IDAssignment, Guid IDSchedule)
        {
            return (
                from S in this.Context.submissionDTOs
                join A in this.Context.assignmentDTOs on S.IDAssignment equals A.IDAssignment
                join MS in this.Context.masterScheduleDTOs on new { A.IDPeriod, A.IDStage, A.IDSubStage } equals new { MS.IDPeriod, MS.IDStage, MS.IDSubStage }
                join TS in this.Context.transactionScheduleDTOs on new { MS.IDSchedule, S.IDUser } equals new { TS.IDSchedule, TS.IDUser }
                join U in this.Context.userDTOs on TS.IDUser equals U.IDUser

                where !S.StsRc.Equals(BaseConstraint.StsRc.Delete)
                where !S.StsRc.Equals(BaseConstraint.StsRc.Inactive)
                where !A.StsRc.Equals(BaseConstraint.StsRc.Delete)
                where !A.StsRc.Equals(BaseConstraint.StsRc.Inactive)
                where !MS.StsRc.Equals(BaseConstraint.StsRc.Delete)
                where !MS.StsRc.Equals(BaseConstraint.StsRc.Inactive)
                where !TS.StsRc.Equals(BaseConstraint.StsRc.Delete)
                where !TS.StsRc.Equals(BaseConstraint.StsRc.Inactive)

                where A.IDAssignment == IDAssignment
                where MS.IDSchedule.Equals(IDSchedule)

                select new Submission
                {
                    IDAssignment = A.IDAssignment,
                    IDSubmission = S.IDSubmission,
                    IDUser = U.IDUser,
                    NIM = U.Username,
                    Name = U.Name,
                    FilePath = S.FilePath
                }
            ).ToList();
        }
    }
}
