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
    public interface IMasterScheduleRepository : IRepository<MasterScheduleDTO>
    {        
        IEnumerable<MasterScheduleDTO> GetScheduleByPeriodStagePosition(int IDPeriod, int IDStage, string IDPosition);
        IEnumerable<MasterScheduleDTO> GetScheduleByPeriodStage(int IDPeriod, int IDStage);
        IEnumerable<MasterScheduleDTO> GetScheduleByPeriod(int IDPeriod);
        MasterScheduleDTO GetScheduleByIDSchedule(Guid IDSchedule);
        IEnumerable<MasterScheduleDTO> GetScheduleByListIDSchedule(List<Guid> IDScheduleList);
        List<AssignmentSchedule> GetScheduleByPeriodStageSubStage(int IDPeriod, int IDStage, int IDSubStage);
        List<Model.Subdomains.DashboardSubdomain.Staff.MasterSchedule> GetUpcomingSchedule();
    }
    public class MasterScheduleRepository : BaseRepository<MasterScheduleDTO>, IMasterScheduleRepository
    {
        public MasterScheduleRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public IEnumerable<MasterScheduleDTO> GetScheduleByPeriodStagePosition(int IDPeriod, int IDStage, string IDPosition)
        {
            return Context.masterScheduleDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDPeriod == IDPeriod &&
                            x.IDStage == IDStage &&
                            x.IDPosition.Contains(IDPosition)
                );
        }

        public IEnumerable<MasterScheduleDTO> GetScheduleByPeriodStage(int IDPeriod, int IDStage)
        {
            return Context.masterScheduleDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDPeriod == IDPeriod &&
                            x.IDStage == IDStage
                );
        }

        public IEnumerable<MasterScheduleDTO> GetScheduleByPeriod(int IDPeriod)
        {
            return Context.masterScheduleDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDPeriod == IDPeriod
                );
        }

        public MasterScheduleDTO GetScheduleByIDSchedule(Guid IDSchedule)
        {
            return Context.masterScheduleDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) && 
                            x.IDSchedule.Equals(IDSchedule)
                ).FirstOrDefault();
        }

        public IEnumerable<MasterScheduleDTO> GetScheduleByListIDSchedule(List<Guid> IDScheduleList)
        {
            return Context.masterScheduleDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            IDScheduleList.Contains(x.IDSchedule)
                );
        }

        public List<AssignmentSchedule> GetScheduleByPeriodStageSubStage(int IDPeriod, int IDStage, int IDSubStage)
        {
            return (
                from A in this.Context.assignmentDTOs
                join MS in this.Context.masterScheduleDTOs on new { A.IDPeriod, A.IDStage, A.IDSubStage } equals new { MS.IDPeriod, MS.IDStage, MS.IDSubStage }
                join U in this.Context.userDTOs on MS.IDReviewer equals U.IDUser into UX
                from U in UX.DefaultIfEmpty()
                join PR in this.Context.periodDTOs on MS.IDPeriod equals PR.IDPeriod
                join S in this.Context.stageDTOs on MS.IDStage equals S.IDStage
                join SS in this.Context.subStageDTOs on MS.IDSubStage equals SS.IDSubStage into SSX
                from SS in SSX.DefaultIfEmpty()

                where !MS.StsRc.Equals(BaseConstraint.StsRc.Delete)
                where !MS.StsRc.Equals(BaseConstraint.StsRc.Inactive)

                where MS.IDPeriod == IDPeriod
                where MS.IDStage == IDStage
                where MS.IDSubStage == IDSubStage

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
                    TrainerName = U.Name
                }
            ).ToList();
        }

        public List<Model.Subdomains.DashboardSubdomain.Staff.MasterSchedule> GetUpcomingSchedule()
        {
            return (
                from MS in this.Context.masterScheduleDTOs
                join S in this.Context.stageDTOs on MS.IDStage equals S.IDStage
                join SS in this.Context.subStageDTOs on MS.IDSubStage equals SS.IDSubStage into SSX
                from SS in SSX.DefaultIfEmpty()
                where !MS.StsRc.Equals(BaseConstraint.StsRc.Delete)
                where !MS.StsRc.Equals(BaseConstraint.StsRc.Inactive)
                where MS.Date >= DateTime.Now
                select new Model.Subdomains.DashboardSubdomain.Staff.MasterSchedule
                {
                    IDSchedule = MS.IDSchedule,
                    IDPeriod = MS.IDPeriod,
                    IDStage = MS.IDStage,
                    StageName = S.StageName,
                    IDSubStage = MS.IDSubStage,
                    SubStageName = SS.SubStageName,
                    Date = MS.Date,
                    StartTime = MS.StartTime,
                    EndTime = MS.EndTime,
                    Room = MS.Room
                }
            ).ToList();
        }
    }
}
