using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.DashboardSubdomain.Staff;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.DashboardModule
{
    public interface IStaffService
    {
        Period GetCurrentPeriod();
        Stage GetCurrentStage(int IDStage);
        int CountCandidateByPeriod(int IDPeriod);
        List<MasterSchedule> GetUpcomingSchedule();
        List<BlastEmail> GetNotifications();
        List<MasterSchedule> GetAvailableSchedules();
        List<Assignment> GetAvailableAssignments();
        Assignment GetAssignmentByID(int IDAssignment);
    }
    public class StaffService : IStaffService
    {
        private readonly IPeriodRepository periodRepository;
        private readonly IStageRepository stageRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IMasterScheduleRepository masterScheduleRepository;
        private readonly IBlastEmailRepository blastEmailRepository;
        private readonly ISubStageRepository subStageRepository;
        private readonly ITransactionScheduleRepository transactionScheduleRepository;
        private readonly IAssignmentRepository assignmentRepository;

        public StaffService(
            IPeriodRepository periodRepository,
            IStageRepository stageRepository,
            ICandidateRepository candidateRepository,
            IMasterScheduleRepository masterScheduleRepository,
            IBlastEmailRepository blastEmailRepository,
            ISubStageRepository subStageRepository,
            ITransactionScheduleRepository transactionScheduleRepository,
            IAssignmentRepository assignmentRepository)
        {
            this.periodRepository = periodRepository;
            this.stageRepository = stageRepository;
            this.candidateRepository = candidateRepository;
            this.masterScheduleRepository = masterScheduleRepository;
            this.blastEmailRepository = blastEmailRepository;
            this.subStageRepository = subStageRepository;
            this.transactionScheduleRepository = transactionScheduleRepository;
            this.assignmentRepository = assignmentRepository;
        }

        public Period GetCurrentPeriod()
        {
            var periodDTO = periodRepository.GetActivePeriod();

            if (periodDTO != null)
            {
                Period period = new Period();
                period.IDPeriod = periodDTO.IDPeriod;
                period.IDStage = periodDTO.IDStage;
                period.PeriodName = periodDTO.PeriodName;
                period.IsActive = periodDTO.IsActive;
                period.DeadlineStart = periodDTO.DeadlineStart;
                period.DeadlineEnd = periodDTO.DeadlineEnd;

                return period;
            }

            return null;
        }

        public Stage GetCurrentStage(int IDStage)
        {
            var stageDTO = stageRepository.GetStageById(IDStage);

            if (stageDTO != null)
            {
                Stage stage = new Stage();
                stage.IDStage = IDStage;
                stage.StageName = stageDTO.StageName;
                stage.StageLevel = stageDTO.StageLevel;

                return stage;
            }

            return null;
        }

        public int CountCandidateByPeriod(int IDPeriod)
        {
            return candidateRepository.GetCandidateByPeriod(IDPeriod).Count();
        }

        public List<MasterSchedule> GetUpcomingSchedule()
        {
            return masterScheduleRepository.GetUpcomingSchedule();
        }

        public List<BlastEmail> GetNotifications()
        {
            List<BlastEmail> result = new List<BlastEmail>();

            PeriodDTO period = periodRepository.GetActivePeriod();
            List<BlastEmailDTO> blastEmailDTOs = blastEmailRepository.GetByIDPeriod(period != null ? period.IDPeriod : 0).ToList();

            foreach (var item in blastEmailDTOs)
            {
                BlastEmail blastEmail = new BlastEmail()
                {
                    IDBlastEmail = item.IDBlastEmail,
                    IDPeriod = item.IDPeriod,
                    Subject = item.Subject,
                    Description = item.Description,
                    BlastDateTime = item.BlastDateTime
                };
                result.Add(blastEmail);
            }

            result = result.OrderByDescending(x => x.BlastDateTime).ToList();

            return result;
        }

        public List<MasterSchedule> GetAvailableSchedules()
        {
            List<MasterSchedule> result = new List<MasterSchedule>();
            List<StageDTO> stageDTOs = stageRepository.FindAll().ToList();
            List<SubStageDTO> subStageDTOs = subStageRepository.FindAll().ToList();
            PeriodDTO period = periodRepository.GetActivePeriod();

            List<MasterScheduleDTO> scheduleDTOs = masterScheduleRepository.GetScheduleByPeriod(period.IDPeriod)
                .Where(x => (x.Date.Date.CompareTo(DateTime.Now.Date) > 0 || x.Date.Date.CompareTo(DateTime.Now.Date) == 0 && x.StartTime.CompareTo(DateTime.Now.TimeOfDay) > 0))
                .OrderBy(x => x.Date).ToList();

            List<Guid> IDScheduleList = scheduleDTOs.Select(x => x.IDSchedule).ToList();
            List<TransactionScheduleDTO> transactionScheduleDTOs = transactionScheduleRepository.GetByIDScheduleList(IDScheduleList).ToList();

            foreach (MasterScheduleDTO item in scheduleDTOs)
            {
                StageDTO stage = stageDTOs.Where(x => x.IDStage == item.IDStage).FirstOrDefault();
                SubStageDTO subStage = subStageDTOs.Where(x => x.IDSubStage == item.IDSubStage).FirstOrDefault();
                int qty = transactionScheduleDTOs != null ? transactionScheduleDTOs.Where(x => x.IDSchedule.Equals(item.IDSchedule)).Count() : 0;

                MasterSchedule schedule = new MasterSchedule()
                {
                    IDSchedule = item.IDSchedule,
                    IDPeriod = item.IDPeriod,
                    IDStage = item.IDStage,
                    IDSubStage = item.IDSubStage,
                    Date = item.Date,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Limit = item.Limit,
                    Room = item.Room,
                    Qty = qty,
                    StageName = stage != null ? stage.StageName : "",
                    SubStageName = subStage != null ? subStage.SubStageName : "",
                };

                result.Add(schedule);
            }

            result = result.OrderBy(x => x.Date)
                .ThenBy(x => x.StartTime)
                .ThenBy(x => x.IDStage)
                .ThenBy(x => x.IDSubStage)
                .ToList();

            return result;
        }

        public List<Assignment> GetAvailableAssignments()
        {
            List<Assignment> result = new List<Assignment>();

            List<StageDTO> stageDTOs = stageRepository.FindAll().ToList();
            List<SubStageDTO> subStageDTOs = subStageRepository.FindAll().ToList();

            PeriodDTO period = periodRepository.GetActivePeriod();
            List<AssignmentDTO> assignmentDTOs = assignmentRepository.GetAssignmentByIdPeriod(period != null ? period.IDPeriod : 0).ToList();
            List<MasterScheduleDTO> scheduleDTOs = masterScheduleRepository.GetScheduleByPeriod(period != null ? period.IDPeriod : 0).ToList();

            foreach (var item in assignmentDTOs)
            {
                MasterScheduleDTO schedule = scheduleDTOs.Where(x => x.IDStage == item.IDStage &&
                                                                    x.IDSubStage == item.IDSubStage).FirstOrDefault();
                if (schedule != null)
                {
                    StageDTO stage = stageDTOs.Where(x => x.IDStage == item.IDStage).FirstOrDefault();
                    SubStageDTO subStage = subStageDTOs.Where(x => x.IDSubStage == item.IDSubStage).FirstOrDefault();

                    Assignment assignment = new Assignment()
                    {
                        IDAssignment = item.IDAssignment,
                        IDPeriod = item.IDPeriod,
                        IDStage = item.IDStage,
                        IDSubStage = item.IDSubStage,
                        DeadlineStartDate = item.DeadlineStart,
                        DeadlineEndDate = item.DeadlineEnd,
                        StageName = stage != null ? stage.StageName : "",
                        SubStageName = subStage != null ? subStage.SubStageName : ""
                    };

                    result.Add(assignment);
                }
            }

            result = result.OrderBy(x => x.DeadlineStartDate).ThenBy(x => x.DeadlineEndDate).ToList();

            return result;
        }

        public Assignment GetAssignmentByID(int IDAssignment)
        {
            AssignmentDTO result = assignmentRepository.GetAssignmentById(IDAssignment);
            return new Assignment()
            {
                IDAssignment = result.IDAssignment,
                IDPeriod = result.IDPeriod,
                IDStage = result.IDStage,
                IDSubStage = result.IDSubStage,
                AssignmentFileName = result.FilePath,
                Notes = result.Notes,
                DeadlineStartDate = result.DeadlineStart,
                DeadlineEndDate = result.DeadlineEnd
            };
        }
    }
}
