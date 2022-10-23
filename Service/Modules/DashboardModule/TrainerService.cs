using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.DashboardSubdomain.Trainer;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.DashboardModule
{
    public interface ITrainerService
    {
        int CountCandidateByPeriod();
        string GetCurrentPeriod();
        Stage GetCurrentStage();
        List<MasterSchedule> GetTrainerAssignmentSchedule(Guid IDUser);
        List<MasterSchedule> GetAvailableAssignmentSchedule();
        List<CandidateSchedule> GetTodayInterview();
    }
    public class TrainerService : ITrainerService
    {
        private readonly IPeriodRepository periodRepository;
        private readonly IStageRepository stageRepository;
        private readonly ISubStageRepository subStageRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IMasterScheduleRepository masterScheduleRepository;
        private readonly ITransactionScheduleRepository transactionScheduleRepository;
        private readonly IAssignmentRepository assignmentRepository;
        private readonly ISubmissionRepository submissionRepository;
        private readonly IPositionRepository positionRepository;
        private readonly IUserRepository userRepository;

        public TrainerService(
            IPeriodRepository periodRepository,
            IStageRepository stageRepository, 
            ISubStageRepository subStageRepository,
            ICandidateRepository candidateRepository,
            IMasterScheduleRepository masterScheduleRepository,
            ITransactionScheduleRepository transactionScheduleRepository,
            IAssignmentRepository assignmentRepository,
            ISubmissionRepository submissionRepository,
            IPositionRepository positionRepository,
            IUserRepository userRepository
            )
        {
            this.periodRepository = periodRepository;
            this.stageRepository = stageRepository;
            this.subStageRepository = subStageRepository;
            this.candidateRepository = candidateRepository;
            this.masterScheduleRepository = masterScheduleRepository;
            this.transactionScheduleRepository = transactionScheduleRepository;
            this.assignmentRepository = assignmentRepository;
            this.submissionRepository = submissionRepository;
            this.positionRepository = positionRepository;
            this.userRepository = userRepository;
        }

        public int CountCandidateByPeriod()
        {
            PeriodDTO period = periodRepository.GetActivePeriod();
            if (period != null)
            {
                return candidateRepository.GetCandidateByPeriod(period.IDPeriod).Count();
            }

            return 0;
        }

        public string GetCurrentPeriod()
        {
            PeriodDTO period = periodRepository.GetActivePeriod();

            if (period != null)
            {
                return period.PeriodName;
            }

            return "-";
        }

        public Stage GetCurrentStage()
        {
            PeriodDTO period = periodRepository.GetActivePeriod();
            StageDTO stage = stageRepository.GetStageById(period.IDStage);
            return new Stage()
            {
                IDStage = stage.IDStage,
                StageName = stage.StageName,
                StageLevel = stage.StageLevel
            };
        }      

        public List<MasterSchedule> GetTrainerAssignmentSchedule(Guid IDUser)
        {
            List<MasterSchedule> result = new List<MasterSchedule>();

            // Get Base Data
            PeriodDTO period = periodRepository.GetActivePeriod();
            StageDTO stage = stageRepository.GetStageById(period != null ? period.IDStage : 0);
            List<SubStageDTO> subStageDTOs = subStageRepository.GetSubStageByStageID(stage.IDStage).ToList();

            // Get Assignment
            List<AssignmentDTO> assignmentDTOs = assignmentRepository.GetAssignmentByPeriodStage(period.IDPeriod, stage.IDStage).ToList();
            List<int> IDSubStageList = assignmentDTOs.Select(x => x.IDSubStage).ToList();
            List<int> IDAssignmentList = assignmentDTOs.Select(x => x.IDAssignment).ToList();

            // Get Schedule
            List<MasterScheduleDTO> masterScheduleDTOs = masterScheduleRepository.GetScheduleByPeriodStage(period.IDPeriod, stage.IDStage).ToList();
            masterScheduleDTOs = masterScheduleDTOs.Where(x => x.IDReviewer.Equals(IDUser) && IDSubStageList.Contains(x.IDSubStage)).ToList();

            // Get Transaction Schedule
            List<Guid> IDScheduleList = masterScheduleDTOs.Select(x => x.IDSchedule).ToList();
            List<TransactionScheduleDTO> transactionScheduleDTOs = transactionScheduleRepository.GetByIDScheduleList(IDScheduleList).ToList();            

            // Get Submission
            List<SubmissionDTO> submissionDTOs = submissionRepository.GetSubmissionByIDAssignmentList(IDAssignmentList).ToList();

            foreach (var item in masterScheduleDTOs)
            {
                List<TransactionScheduleDTO> transaction = transactionScheduleDTOs.Where(x => x.IDSchedule.Equals(item.IDSchedule)).ToList();
                int qty = transaction != null ? transaction.Count() : 0;
                List<Guid> IDUserList = transaction.Select(x => x.IDUser).ToList();

                SubStageDTO subStage = subStageDTOs.Where(x => x.IDSubStage == item.IDSubStage).FirstOrDefault();
                AssignmentDTO assignment = assignmentDTOs.Where(x => x.IDSubStage == item.IDSubStage).FirstOrDefault();
                int countSubmission = submissionDTOs.Where(x => x.IDAssignment == assignment.IDAssignment && IDUserList.Contains(x.IDUser)).Count();

                MasterSchedule schedule = new MasterSchedule()
                {
                    IDSchedule = item.IDSchedule,
                    IDPeriod = item.IDPeriod,
                    IDStage = item.IDStage,
                    IDSubStage = item.IDSubStage,
                    IDPosition = item.IDPosition,
                    IDReviewer = item.IDReviewer ?? Guid.NewGuid(),
                    IDAssignment = assignment.IDAssignment,
                    Date = item.Date,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Qty = qty,
                    Limit = item.Limit,
                    Room = item.Room,
                    CountSubmission = countSubmission,
                    StageName = stage.StageName,
                    SubStageName = subStage != null ? subStage.SubStageName : "",
                    DeadlineStart = assignment.DeadlineStart,
                    DeadlineEnd = assignment.DeadlineEnd
                };

                result.Add(schedule);
            }

            return result;
        }

        public List<MasterSchedule> GetAvailableAssignmentSchedule()
        {
            List<MasterSchedule> result = new List<MasterSchedule>();

            // Get Base Data
            PeriodDTO period = periodRepository.GetActivePeriod();
            StageDTO stage = stageRepository.GetStageById(period != null ? period.IDStage : 0);
            List<SubStageDTO> subStageDTOs = subStageRepository.GetSubStageByStageID(stage.IDStage).ToList();

            // Get Assignment
            List<AssignmentDTO> assignmentDTOs = assignmentRepository.GetAssignmentByPeriodStage(period.IDPeriod, stage.IDStage).ToList();
            assignmentDTOs = assignmentDTOs.Where(x => DateTime.Now.Date.CompareTo(x.DeadlineEnd.Date) > 0).ToList();
            List<int> IDSubStageList = assignmentDTOs.Select(x => x.IDSubStage).ToList();
            List<int> IDAssignmentList = assignmentDTOs.Select(x => x.IDAssignment).ToList();

            // Get Schedule
            List<MasterScheduleDTO> masterScheduleDTOs = masterScheduleRepository.GetScheduleByPeriodStage(period.IDPeriod, stage.IDStage).ToList();
            masterScheduleDTOs = masterScheduleDTOs.Where(x => x.IDReviewer.Equals(null) && IDSubStageList.Contains(x.IDSubStage)).ToList();

            // Get Transaction Schedule
            List<Guid> IDScheduleList = masterScheduleDTOs.Select(x => x.IDSchedule).ToList();
            List<TransactionScheduleDTO> transactionScheduleDTOs = transactionScheduleRepository.GetByIDScheduleList(IDScheduleList).ToList();
            //List<Guid> IDUserList = transactionScheduleDTOs.Select(x => x.IDUser).ToList();

            // Get Submission
            List<SubmissionDTO> submissionDTOs = submissionRepository.GetSubmissionByIDAssignmentList(IDAssignmentList).ToList();
            //submissionDTOs = submissionDTOs.Where(x => IDUserList.Contains(x.IDUser)).ToList();

            foreach (var item in masterScheduleDTOs)
            {
                List<TransactionScheduleDTO> transaction = transactionScheduleDTOs.Where(x => x.IDSchedule.Equals(item.IDSchedule)).ToList();
                int qty = transaction != null ? transaction.Count() : 0;
                List<Guid> IDUserList = transaction.Select(x => x.IDUser).ToList();


                SubStageDTO subStage = subStageDTOs.Where(x => x.IDSubStage == item.IDSubStage).FirstOrDefault();
                AssignmentDTO assignment = assignmentDTOs.Where(x => x.IDSubStage == item.IDSubStage).FirstOrDefault();
                int countSubmission = submissionDTOs.Where(x => x.IDAssignment == assignment.IDAssignment && IDUserList.Contains(x.IDUser)).Count();

                MasterSchedule schedule = new MasterSchedule()
                {
                    IDSchedule = item.IDSchedule,
                    IDPeriod = item.IDPeriod,
                    IDStage = item.IDStage,
                    IDSubStage = item.IDSubStage,
                    IDPosition = item.IDPosition,                    
                    IDAssignment = assignment.IDAssignment,
                    Date = item.Date,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Qty = qty,
                    Limit = item.Limit,
                    Room = item.Room,
                    CountSubmission = countSubmission,
                    StageName = stage.StageName,
                    SubStageName = subStage != null ? subStage.SubStageName : "",
                    DeadlineStart = assignment.DeadlineStart,
                    DeadlineEnd = assignment.DeadlineEnd
                };

                result.Add(schedule);
            }

            return result;
        }

        public List<CandidateSchedule> GetTodayInterview()
        {
            List<CandidateSchedule> result = new List<CandidateSchedule>();
            PeriodDTO period = periodRepository.GetActivePeriod();
            List<PositionDTO> positionDTOs = positionRepository.GetAllPosition().ToList();            

            // Get Schedule
            List<MasterScheduleDTO> scheduleDTOs = masterScheduleRepository.GetScheduleByPeriodStage(period.IDPeriod, 4).ToList();
            scheduleDTOs = scheduleDTOs.Where(x => x.Date.Date.CompareTo(DateTime.Now.Date) == 0).ToList();
            List<Guid> IDScheduleList = scheduleDTOs.Select(x => x.IDSchedule).ToList();

            // Get Logic Test Score
            //List<QuestionDTO> questionDTOs = questionRepository.GetQuestionListByPeriodStageSubstage(period.IDPeriod, 4, 0).ToList();
            //List<Guid> IDQuestionList = questionDTOs.Select(x => x.IDQuestion).ToList();
            //List<CandidateScoreDTO> candidateScoreDTOs = candidateScoreRepository.GetCandidateScoreListByQuestionIDList(IDQuestionList).ToList();

            // Get Mapping User - Schedule
            List<TransactionScheduleDTO> transactionScheduleDTOs = transactionScheduleRepository.GetByIDScheduleList(IDScheduleList).ToList();
            List<Guid> IDUserList = transactionScheduleDTOs.Select(x => x.IDUser).ToList();

            // Get Candidate
            List<UserDTO> userDTOs = userRepository.GetUserByUserIDList(IDUserList).ToList();
            List<string> NIMList = userDTOs.Select(x => x.Username).ToList();
            List<CandidateDTO> candidateDTOs = candidateRepository.GetCandidateByNIMList(NIMList).ToList();            

            foreach(var item in candidateDTOs)
            {
                //List<CandidateScoreDTO> scores = candidateScoreDTOs.Where(x => x.IDCandidate.Equals(item.IDCandidate)).ToList();
                //if(scores == null || scores.Count == 0)
                //{
                    PositionDTO position = positionDTOs.Where(x => x.IDPosition.Equals(item.IDPosition)).FirstOrDefault();
                    UserDTO user = userDTOs.Where(x => x.Username.Equals(item.NIM)).FirstOrDefault();

                    TransactionScheduleDTO transaction = transactionScheduleDTOs.Where(x => x.IDUser.Equals(user.IDUser)).FirstOrDefault();
                    MasterScheduleDTO schedule = scheduleDTOs.Where(x => x.IDSchedule.Equals(transaction.IDSchedule)).FirstOrDefault();

                    CandidateSchedule candidate = new CandidateSchedule()
                    {
                        IDSchedule = schedule.IDSchedule,
                        IDCandidate = item.IDCandidate,
                        NIM = item.NIM,
                        Name = user.Name,
                        PositionName = position.PositionName,
                        StartTime = schedule.StartTime,
                        EndTime = schedule.EndTime,
                        Room = schedule.Room
                    };

                    result.Add(candidate);
                //}                
            }

            return result;
        }
    }
}
