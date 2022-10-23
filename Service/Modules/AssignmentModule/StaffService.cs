using Helper.DropdownHelper;
using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.AssignmentSubdomain.Staff;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.AssignmentModule
{
    public interface IStaffService
    {
        List<Period> GetPeriods();
        Period GetCurrentPeriod();
        int CountScheduleByActivePeriod(int IDPeriod);
        List<Stage> GetStages();
        List<Stage> GetStagesInScheduleNoAssignment(int IDPeriod);
        List<SubStage> GetSubStagesByStageId(int IDStage);
        List<SubStage> GetSubStagesInScheduleNoAssignment(int IDPeriod, int IDStage);
        List<AssignmentSchedule> GetAssignmentsByPeriodStageSubStageId(int IDPeriod, int IDStage, int IDSubStage);
        Assignment GetAssignmentByIDAssignment(int IDAssignment);
        AssignmentFixedData GetAssignmentFixedDataByIDAssignment(int IDAssignment);
        bool InsertAssignment(Assignment assignment);
        bool UpdateAssignment(Assignment assignment);
        string GetAssignmentQuestionFilePathById(int IDAssignment);
        List<Submission> GetSubmissionsByIDAssignmentAndIDSubmission(int IDAssignment, Guid IDSchedule);
        AssignmentSchedule GetAssignmentDetailsIDAssignmentAndIDSubmission(int IDAssignment, Guid IDSchedule);
    }

    public class StaffService : IStaffService
    {
        private readonly IAssignmentRepository assignmentRepository;
        private readonly IPeriodRepository periodRepository;
        private readonly IStageRepository stageRepository;
        private readonly ISubStageRepository subStageRepository;
        private readonly IMasterScheduleRepository masterScheduleRepository;
        private readonly ITransactionScheduleRepository transactionScheduleRepository;
        private readonly ISubmissionRepository submissionRepository;
        private readonly UnitOfWork unitOfWork;

        public StaffService(
            IAssignmentRepository assignmentRepository,
            IPeriodRepository periodRepository,
            IStageRepository stageRepository,
            ISubStageRepository subStageRepository,
            IMasterScheduleRepository masterScheduleRepository,
            ITransactionScheduleRepository transactionScheduleRepository,
            ISubmissionRepository submissionRepository,
            UnitOfWork unitOfWork
        )
        {
            this.assignmentRepository = assignmentRepository;
            this.periodRepository = periodRepository;
            this.stageRepository = stageRepository;
            this.subStageRepository = subStageRepository;
            this.masterScheduleRepository = masterScheduleRepository;
            this.transactionScheduleRepository = transactionScheduleRepository;
            this.submissionRepository = submissionRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<Period> GetPeriods()
        {
            List<Period> periods = new List<Period>();
            var allPeriods = periodRepository.FindAll().ToList();
            foreach (var item in allPeriods)
            {
                periods.Add(new Period()
                {
                    IDPeriod = item.IDPeriod,
                    PeriodName = item.PeriodName
                });
            }
            return periods;
        }

        public Period GetCurrentPeriod()
        {
            var allPeriods = periodRepository.GetActivePeriod();

            Period period = new Period();
            period.IDPeriod = allPeriods.IDPeriod;
            period.PeriodName = allPeriods.PeriodName;

            return period;
        }

        public int CountScheduleByActivePeriod(int IDPeriod)
        {
            var scheduleByActivePeriod = masterScheduleRepository.GetScheduleByPeriod(IDPeriod);
            return scheduleByActivePeriod.Count();
        }

        public List<Stage> GetStages()
        {
            List<Stage> stages = new List<Stage>();
            var allStages = stageRepository.FindAll().ToList();
            foreach (var item in allStages)
            {
                stages.Add(new Stage()
                {
                    IDStage = item.IDStage,
                    StageName = item.StageName
                });
            }
            return stages;
        }

        public List<Stage> GetStagesInScheduleNoAssignment(int IDPeriod)
        {
            List<Stage> stages = new List<Stage>();
            var periodAssignment = assignmentRepository.GetAssignmentByIdPeriod(IDPeriod).Select(x => new { x.IDStage, x.IDSubStage }).Distinct().ToList();
            var stagesInSchedule = masterScheduleRepository.GetScheduleByPeriod(IDPeriod).Select(x => new { x.IDStage, x.IDSubStage }).Distinct().ToList();
            var remainingStageSubStage = stagesInSchedule.Except(periodAssignment).Select(x => x.IDStage).Distinct().ToList();
            var allStages = stageRepository.GetStagesByIDStageList(remainingStageSubStage).ToList();
            foreach (var item in allStages)
            {
                stages.Add(new Stage()
                {
                    IDStage = item.IDStage,
                    StageName = item.StageName
                });
            }
            return stages;
        }

        public List<SubStage> GetSubStagesByStageId(int IDStage)
        {
            List<SubStage> subStages = new List<SubStage>();
            var allSubStages = subStageRepository.GetSubStageByStageID(IDStage);
            foreach (var item in allSubStages)
            {
                subStages.Add(new SubStage()
                {
                    IDSubStage = item.IDSubStage,
                    SubStageName = item.SubStageName
                });
            }
            return subStages;
        }

        public List<SubStage> GetSubStagesInScheduleNoAssignment(int IDPeriod, int IDStage)
        {
            List<SubStage> subStages = new List<SubStage>();
            var periodAssignment = assignmentRepository.GetAssignmentByPeriodStage(IDPeriod, IDStage).Select(x => x.IDSubStage).Distinct().ToList();
            var subStagesInSchedule = masterScheduleRepository.GetScheduleByPeriodStage(IDPeriod, IDStage).Select(x => x.IDSubStage).Distinct().ToList();
            var allSubStages = subStageRepository.GetSubStagesByIDSubStageList(subStagesInSchedule);
            foreach (var item in allSubStages)
            {
                if (!periodAssignment.Contains(item.IDSubStage))
                {
                    subStages.Add(new SubStage()
                    {
                        IDSubStage = item.IDSubStage,
                        SubStageName = item.SubStageName
                    });
                }
            }
            return subStages;
        }

        public List<AssignmentSchedule> GetAssignmentsByPeriodStageSubStageId(int IDPeriod, int IDStage, int IDSubStage)
        {
            return masterScheduleRepository.GetScheduleByPeriodStageSubStage(IDPeriod, IDStage, IDSubStage);
        }

        public Assignment GetAssignmentByIDAssignment(int IDAssignment)
        {
            return assignmentRepository.GetAssignmentForUpdate(IDAssignment);
        }

        public AssignmentFixedData GetAssignmentFixedDataByIDAssignment(int IDAssignment)
        {
            return assignmentRepository.GetAssignmentFixedDataByIDAssignment(IDAssignment);
        }

        public bool InsertAssignment(Assignment assignment)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(assignmentRepository).ToUse(ctx);
                    TimeSpan StartTime = new TimeSpan(0, 0, 0);
                    TimeSpan EndTime = new TimeSpan(23, 59, 59);
                    AssignmentDTO assignmentDTO = new AssignmentDTO
                    {
                        IDPeriod = assignment.IDPeriod,
                        IDStage = assignment.IDStage,
                        IDSubStage = DropdownManipulation.NormalizeEmptyDropdown(assignment.IDSubStage),
                        DeadlineStart = assignment.DeadlineStartDate.Value + StartTime,
                        DeadlineEnd = assignment.DeadlineEndDate.Value + EndTime,
                        Notes = assignment.Notes.Trim().Replace("\n", "<br/>"),
                        FilePath = assignment.AssignmentFileName
                    };
                    assignmentRepository.Insert(assignmentDTO);
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateAssignment(Assignment assignment)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(assignmentRepository).ToUse(ctx);
                    AssignmentDTO assignmentDTO = assignmentRepository.GetAssignmentById(assignment.IDAssignment);
                    TimeSpan StartTime = new TimeSpan(0, 0, 0);
                    TimeSpan EndTime = new TimeSpan(23, 59, 59);
                    assignmentDTO.IDPeriod = assignment.IDPeriod;
                    assignmentDTO.IDStage = assignment.IDStage;
                    assignmentDTO.IDSubStage = DropdownManipulation.NormalizeEmptyDropdown(assignment.IDSubStage);
                    assignmentDTO.DeadlineStart = assignment.DeadlineStartDate.Value + StartTime;
                    assignmentDTO.DeadlineEnd = assignment.DeadlineEndDate.Value + EndTime;
                    assignmentDTO.Notes = assignment.Notes.Trim().Replace("\n", "<br/>");
                    if(assignment.AssignmentFile != null)
                        assignmentDTO.FilePath = assignment.AssignmentFileName;
                    assignmentRepository.Update(assignmentDTO);
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetAssignmentQuestionFilePathById(int IDAssignment)
        {
            return assignmentRepository.GetAssignmentQuestionFilePathById(IDAssignment);
        }

        public List<Submission> GetSubmissionsByIDAssignmentAndIDSubmission(int IDAssignment, Guid IDSchedule)
        {
            return submissionRepository.GetSubmissionsByIDAssignmentAndIDSubmission(IDAssignment, IDSchedule);
        }

        public AssignmentSchedule GetAssignmentDetailsIDAssignmentAndIDSubmission(int IDAssignment, Guid IDSchedule)
        {
            var schedule = assignmentRepository.GetAssignmentDetailsIDAssignmentAndIDSubmission(IDAssignment, IDSchedule);
            schedule.Quantity = transactionScheduleRepository.GetScheduleStudentCountById(IDSchedule);
            return schedule;
        }
    }
}
