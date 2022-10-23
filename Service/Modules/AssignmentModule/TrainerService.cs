using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.AssignmentSubdomain.Trainer;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.AssignmentModule
{
    public interface ITrainerService
    {
        MasterSchedule GetScheduleDetail(Guid IDSchedule, int IDAssignment);
        Assignment GetAssignment(int IDAssignment);
        List<CandidateAssignment> GetCandidateList(Guid IDSchedule, int IDAssignment);
    }

    public class TrainerService : ITrainerService
    {
        private readonly IPeriodRepository periodRepository;
        private readonly IStageRepository stageRepository;
        private readonly ISubStageRepository subStageRepository;
        private readonly IMasterScheduleRepository masterScheduleRepository;
        private readonly ITransactionScheduleRepository transactionScheduleRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly ISubmissionRepository submissionRepository;
        private readonly IScoringComponentRepository scoringComponentRepository;
        private readonly ICandidateScoreRepository candidateScoreRepository;
        private readonly IUserRepository userRepository;
        private readonly IAssignmentRepository assignmentRepository;

        public TrainerService(IPeriodRepository periodRepository,
            IStageRepository stageRepository,
            ISubStageRepository subStageRepository,
            IMasterScheduleRepository masterScheduleRepository,
            ITransactionScheduleRepository transactionScheduleRepository,
            ICandidateRepository candidateRepository,
            ISubmissionRepository submissionRepository,
            IScoringComponentRepository scoringComponentRepository,
            ICandidateScoreRepository candidateScoreRepository,
            IUserRepository userRepository,
            IAssignmentRepository assignmentRepository)
        {
            this.periodRepository = periodRepository;
            this.stageRepository = stageRepository;
            this.subStageRepository = subStageRepository;
            this.masterScheduleRepository = masterScheduleRepository;
            this.transactionScheduleRepository = transactionScheduleRepository;
            this.candidateRepository = candidateRepository;
            this.submissionRepository = submissionRepository;
            this.scoringComponentRepository = scoringComponentRepository;
            this.candidateScoreRepository = candidateScoreRepository;
            this.userRepository = userRepository;
            this.assignmentRepository = assignmentRepository;
        }                  

        public MasterSchedule GetScheduleDetail(Guid IDSchedule, int IDAssignment)
        {            
            // Get Schedule
            MasterScheduleDTO scheduleDTO = masterScheduleRepository.GetScheduleByIDSchedule(IDSchedule);
            List<TransactionScheduleDTO> transactionScheduleDTOs = transactionScheduleRepository.GetByIDSchedule(IDSchedule).ToList();
            List<Guid> IDScheduleUserList = transactionScheduleDTOs.Select(x => x.IDUser).ToList();
            int TotalCandidate = transactionScheduleDTOs.Count();

            // Get Total Submission            
            List<SubmissionDTO> submissionDTOs = submissionRepository.GetSubmissionByIDAssignment(IDAssignment).ToList();
            submissionDTOs = submissionDTOs.Where(x => IDScheduleUserList.Contains(x.IDUser)).ToList();
            List<Guid> IDUserList = submissionDTOs.Select(x => x.IDUser).ToList();
            int TotalSubmission = submissionDTOs.Count();

            // Get User
            List<UserDTO> userDTOs = userRepository.GetUserByUserIDList(IDUserList).ToList();
            List<string> UsernameList = userDTOs.Select(x => x.Username).ToList();

            // Get Candidate
            List<CandidateDTO> candidateDTOs = candidateRepository.GetCandidateByNIMList(UsernameList).ToList();
            List<Guid> IDCandidateList = candidateDTOs.Select(x => x.IDCandidate).ToList();

            // Get Stage, SubStage            
            StageDTO stage = stageRepository.GetStageById(scheduleDTO.IDStage);
            SubStageDTO subStage = subStageRepository.GetSubStageByID(scheduleDTO.IDSubStage);

            // Get Question
            List<ScoringComponentDTO> questionDTOs = scoringComponentRepository.GetScoringComponentListByPeriodStageSubstage(scheduleDTO.IDPeriod, scheduleDTO.IDStage, scheduleDTO.IDSubStage).ToList();
            int TotalQuestion = questionDTOs.Count();
            List<CandidateScoreDTO> candidateScoreDTOs = candidateScoreRepository.GetCandidateScoreListByQuestionIDAndCandidateIdList(questionDTOs, IDCandidateList).ToList();

            // Get Status    
            bool Status = true;
            foreach(var candidate in candidateDTOs)
            {
                int CountCandidateQuestion = candidateScoreDTOs.Where(x => x.IDCandidate.Equals(candidate.IDCandidate)).Count();
                if(CountCandidateQuestion < TotalQuestion)
                {
                    Status = false;
                    break;
                }
            }

            return new MasterSchedule()
            {
                IDSchedule = IDSchedule,
                IDAssignment = IDAssignment,
                IDStage = scheduleDTO.IDStage,
                IDSubStage = scheduleDTO.IDSubStage,
                Room = scheduleDTO.Room,
                Date = scheduleDTO.Date,
                StartTime = scheduleDTO.StartTime,
                EndTime = scheduleDTO.EndTime,
                StageName = stage != null ? stage.StageName : "",
                SubStageName = subStage != null ? subStage.SubStageName : "",
                CountSubmission = TotalSubmission,
                Qty = TotalCandidate,
                Status = Status
            };
        }

        public Assignment GetAssignment(int IDAssignment)
        {
            AssignmentDTO assignmentDTO = assignmentRepository.GetAssignmentById(IDAssignment);
            return new Assignment()
            {
                IDAssignment = IDAssignment,
                AssignmentFileName = assignmentDTO.FilePath,
                Notes = assignmentDTO.Notes
            };
        }

        public List<CandidateAssignment> GetCandidateList(Guid IDSchedule, int IDAssignment)
        {
            List<CandidateAssignment> result = new List<CandidateAssignment>();

            // Get Schedule
            MasterScheduleDTO scheduleDTO = masterScheduleRepository.GetScheduleByIDSchedule(IDSchedule);
            List<TransactionScheduleDTO> transactionScheduleDTOs = transactionScheduleRepository.GetByIDSchedule(IDSchedule).ToList();
            List<Guid> IDUserList = transactionScheduleDTOs.Select(x => x.IDUser).ToList();

            // Get Total Submission            
            List<SubmissionDTO> submissionDTOs = submissionRepository.GetSubmissionByIDAssignment(IDAssignment).ToList();
            submissionDTOs = submissionDTOs.Where(x => IDUserList.Contains(x.IDUser)).ToList();
            List<Guid> IDSubmissionUserList = transactionScheduleDTOs.Select(x => x.IDUser).ToList();

            // Get User
            List<UserDTO> userDTOs = userRepository.GetUserByUserIDList(IDSubmissionUserList).ToList();
            List<string> UsernameList = userDTOs.Select(x => x.Username).ToList();

            // Get Candidate
            List<CandidateDTO> candidateDTOs = candidateRepository.GetCandidateByNIMList(UsernameList).ToList();
            List<Guid> IDCandidateList = candidateDTOs.Select(x => x.IDCandidate).ToList();

            // Get Question
            List<ScoringComponentDTO> questionDTOs = scoringComponentRepository.GetScoringComponentListByPeriodStageSubstage(scheduleDTO.IDPeriod, scheduleDTO.IDStage, scheduleDTO.IDSubStage).ToList();
            int TotalQuestion = questionDTOs.Count();
            List<CandidateScoreDTO> candidateScoreDTOs = candidateScoreRepository.GetCandidateScoreListByQuestionIDAndCandidateIdList(questionDTOs, IDCandidateList).ToList();

            foreach (var item in candidateDTOs)
            {
                UserDTO user = userDTOs.Where(x => x.Username.Equals(item.NIM)).FirstOrDefault();
                SubmissionDTO submission = submissionDTOs.Where(x => x.IDUser.Equals(user.IDUser)).FirstOrDefault();
                int CountCandidateQuestion = candidateScoreDTOs.Where(x => x.IDCandidate.Equals(item.IDCandidate)).Count();

                CandidateAssignment candidate = new CandidateAssignment()
                {
                    IDCandidate = item.IDCandidate,
                    Name = user != null ? user.Name : "",
                    NIM = item.NIM,
                    ScoringStatus = CountCandidateQuestion == TotalQuestion,
                    SubmissionFilePath = submission != null ? submission.FilePath : ""
                };

                result.Add(candidate);
            }

            result = result.OrderBy(x => x.ScoringStatus).ThenBy(x => x.Name).ToList();

            return result;
        }
    }
}
