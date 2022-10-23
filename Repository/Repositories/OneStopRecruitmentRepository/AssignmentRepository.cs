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
    public interface IAssignmentRepository : IRepository<AssignmentDTO>
    {
        AssignmentDTO GetAssignmentById(int IDAssignment);
        IEnumerable<AssignmentDTO> GetAssignmentByIdPeriod(int IDPeriod);
        IEnumerable<AssignmentDTO> GetAssignmentByPeriodStage(int IDPeriod, int IDStage);
        AssignmentDTO GetAssignmentByPeriodStageSubStage(int IDPeriod, int IDStage, int IDSubStage);
        Assignment GetAssignmentForUpdate(int IDAssignment);
        AssignmentFixedData GetAssignmentFixedDataByIDAssignment(int IDAssignment);
        string GetAssignmentQuestionFilePathById(int IDAssignment);
        AssignmentSchedule GetAssignmentDetailsIDAssignmentAndIDSubmission(int IDAssignment, Guid IDSchedule);
    }

    public class AssignmentRepository : BaseRepository<AssignmentDTO>, IAssignmentRepository
    {
        public AssignmentRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public AssignmentDTO GetAssignmentById(int IDAssignment)
        {
            return Context.assignmentDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDAssignment == IDAssignment
                ).FirstOrDefault();
        }

        public IEnumerable<AssignmentDTO> GetAssignmentByIdPeriod(int IDPeriod)
        {
            return Context.assignmentDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDPeriod == IDPeriod
                );
        }

        public IEnumerable<AssignmentDTO> GetAssignmentByPeriodStage(int IDPeriod, int IDStage)
        {
            return Context.assignmentDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDPeriod == IDPeriod &&
                            x.IDStage == IDStage
                );
        }

        public AssignmentDTO GetAssignmentByPeriodStageSubStage(int IDPeriod, int IDStage, int IDSubStage)
        {
            return Context.assignmentDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDPeriod == IDPeriod &&
                            x.IDStage == IDStage &&
                            x.IDSubStage == IDSubStage
                ).FirstOrDefault();
        }

        public Assignment GetAssignmentForUpdate(int IDAssignment)
        {
            return (
                from A in this.Context.assignmentDTOs
                join P in this.Context.periodDTOs on A.IDPeriod equals P.IDPeriod
                join S in this.Context.stageDTOs on A.IDStage equals S.IDStage
                join SS in this.Context.subStageDTOs on A.IDSubStage equals SS.IDSubStage into SSX
                from SS in SSX.DefaultIfEmpty()
                where !A.StsRc.Equals(BaseConstraint.StsRc.Delete)
                where !A.StsRc.Equals(BaseConstraint.StsRc.Inactive)
                where A.IDAssignment == IDAssignment
                select new Assignment
                {
                    IDAssignment = A.IDAssignment,
                    IDPeriod = A.IDPeriod,
                    PeriodName = P.PeriodName,
                    IDStage = A.IDStage,
                    StageName = S.StageName,
                    IDSubStage = A.IDSubStage != 0 ? A.IDSubStage : -1,
                    SubStageName = SS.SubStageName,
                    DeadlineStartDate = A.DeadlineStart,
                    DeadlineEndDate = A.DeadlineEnd,
                    Notes = A.Notes,
                    AssignmentFileName = A.FilePath
                }
            ).FirstOrDefault();
        }

        public AssignmentFixedData GetAssignmentFixedDataByIDAssignment(int IDAssignment)
        {
            return (
                from A in this.Context.assignmentDTOs
                join P in this.Context.periodDTOs on A.IDPeriod equals P.IDPeriod
                join S in this.Context.stageDTOs on A.IDStage equals S.IDStage
                join SS in this.Context.subStageDTOs on A.IDSubStage equals SS.IDSubStage into SSX
                from SS in SSX.DefaultIfEmpty()
                where !A.StsRc.Equals(BaseConstraint.StsRc.Delete)
                where !A.StsRc.Equals(BaseConstraint.StsRc.Inactive)
                where A.IDAssignment == IDAssignment
                select new AssignmentFixedData
                {
                    IDAssignment = A.IDAssignment,
                    FilePath = A.FilePath,
                    IDPeriod = A.IDPeriod,
                    PeriodName = P.PeriodName,
                    IDStage = A.IDStage,
                    StageName = S.StageName,
                    IDSubStage = A.IDSubStage != 0 ? A.IDSubStage : -1,
                    SubStageName = SS.SubStageName
                }
            ).FirstOrDefault();
        }

        public string GetAssignmentQuestionFilePathById(int IDAssignment)
        {
            return Context.assignmentDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDAssignment == IDAssignment)
                .Select(x => x.FilePath)
                .FirstOrDefault();
        }

        public AssignmentSchedule GetAssignmentDetailsIDAssignmentAndIDSubmission(int IDAssignment, Guid IDSchedule)
        {
            return (
                from A in this.Context.assignmentDTOs
                join MS in this.Context.masterScheduleDTOs on new { A.IDPeriod, A.IDStage, A.IDSubStage } equals new { MS.IDPeriod, MS.IDStage, MS.IDSubStage }
                join PR in this.Context.periodDTOs on MS.IDPeriod equals PR.IDPeriod
                join S in this.Context.stageDTOs on MS.IDStage equals S.IDStage
                join SS in this.Context.subStageDTOs on MS.IDSubStage equals SS.IDSubStage into SSX
                from SS in SSX.DefaultIfEmpty()
                join T in this.Context.userDTOs on MS.IDReviewer equals T.IDUser into TX
                from T in TX.DefaultIfEmpty()

                where !MS.StsRc.Equals(BaseConstraint.StsRc.Delete)
                where !MS.StsRc.Equals(BaseConstraint.StsRc.Inactive)

                where MS.IDSchedule.Equals(IDSchedule)
                where A.IDAssignment == IDAssignment

                select new AssignmentSchedule
                {
                    IDSchedule = MS.IDSchedule,
                    IDAssignment = A.IDAssignment,
                    IDPeriod = MS.IDPeriod,
                    PeriodName = PR.PeriodName,
                    IDStage = MS.IDStage,
                    StageName = S.StageName,
                    IDSubStage = MS.IDSubStage,
                    SubStageName = SS.SubStageName,
                    ScheduleDate = MS.Date,
                    ScheduleStartTime = MS.StartTime,
                    ScheduleEndTime = MS.EndTime,
                    Room = MS.Room,
                    Limit = MS.Limit,
                    DeadlineStartDate = A.DeadlineStart,
                    DeadlineEndDate = A.DeadlineEnd,
                    IDTrainer = MS.IDReviewer.Value,
                    TrainerName = T.Name
                }
            ).FirstOrDefault();
        }
    }
}
