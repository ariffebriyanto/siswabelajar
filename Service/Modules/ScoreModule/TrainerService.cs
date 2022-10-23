using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.ScoreSubdomain.Trainer;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.ScoreModule
{
    public interface ITrainerService
    {
        Candidate GetCandidateData(Guid IDCandidate, int IDAssignment);
        List<ScoringComponent> GetQuestionList(Guid IDCandidate, int IDAssignment);
        bool SaveCandidateScore(List<ScoringComponent> QuestionList, Guid IDCandidate, Guid IDSchedule, Guid IDUser);
    }

    public class TrainerService : ITrainerService
    {
        private readonly IScoringComponentRepository scoringComponentRepository;
        private readonly IScoringComponentTypeRepository scoringComponentTypeRepository;
        private readonly ICandidateScoreRepository candidateScoreRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IUserRepository userRepository;
        private readonly IPositionRepository positionRepository;
        private readonly IAssignmentRepository assignmentRepository;
        private readonly ISubmissionRepository submissionRepository;
        private readonly IPeriodRepository periodRepository;
        private readonly IMasterScheduleRepository masterScheduleRepository;
        private readonly ITransactionScheduleRepository transactionScheduleRepository;
        private readonly UnitOfWork unitOfWork;

        public TrainerService(
            IScoringComponentRepository scoringComponentRepository,
            IScoringComponentTypeRepository scoringComponentTypeRepository,
            ICandidateScoreRepository candidateScoreRepository,
            ICandidateRepository candidateRepository,
            IUserRepository userRepository,
            IPositionRepository positionRepository,
            IAssignmentRepository assignmentRepository,
            ISubmissionRepository submissionRepository,
            IPeriodRepository periodRepository,
            IMasterScheduleRepository masterScheduleRepository,
            ITransactionScheduleRepository transactionScheduleRepository,
            UnitOfWork unitOfWork)
        {
            this.scoringComponentRepository = scoringComponentRepository;
            this.scoringComponentTypeRepository = scoringComponentTypeRepository;
            this.candidateScoreRepository = candidateScoreRepository;
            this.candidateRepository = candidateRepository;
            this.userRepository = userRepository;
            this.positionRepository = positionRepository;
            this.assignmentRepository = assignmentRepository;
            this.submissionRepository = submissionRepository;
            this.periodRepository = periodRepository;
            this.masterScheduleRepository = masterScheduleRepository;
            this.transactionScheduleRepository = transactionScheduleRepository;                
            this.unitOfWork = unitOfWork;
        }

        public Candidate GetCandidateData(Guid IDCandidate, int IDAssignment)
        {
            // Get User Data
            CandidateDTO candidateDTO = candidateRepository.FindAll().Where(x => x.IDCandidate.Equals(IDCandidate)).FirstOrDefault();
            UserDTO userDTO = userRepository.GetUserByUsername(candidateDTO.NIM);
            PositionDTO positionDTO = positionRepository.GetPositionById(candidateDTO.IDPosition);

            // Get Assignment            
            AssignmentDTO assignmentDTO = assignmentRepository.GetAssignmentById(IDAssignment);
            SubmissionDTO submissionDTO = submissionRepository.GetSubmissionByIDAssignment(IDAssignment)
                .Where(x => x.IDUser.Equals(userDTO.IDUser)).FirstOrDefault();


            return new Candidate()
            {
                IDCandidate = IDCandidate,
                Name = userDTO.Name,
                NIM = candidateDTO.NIM,
                PositionName = positionDTO.PositionName,
                AssignmentNotes = submissionDTO != null ? submissionDTO.Notes : "",
                SubmissionFilePath = submissionDTO != null ? submissionDTO.FilePath : ""
            };
        }

        public List<ScoringComponent> GetQuestionList(Guid IDCandidate, int IDAssignment)
        {
            List<ScoringComponent> result = new List<ScoringComponent>();

            int IDPeriod = 0;
            int IDStage = 0;
            int IDSubStage = 0;

            if(IDAssignment == -1) 
            {
                PeriodDTO periodDTO = periodRepository.GetActivePeriod();
                if(periodDTO != null)
                {
                    IDPeriod = periodDTO.IDPeriod;
                    IDStage = periodDTO.IDStage;
                }                
            }
            else
            {
                // Get Assignment
                AssignmentDTO assignmentDTO = assignmentRepository.GetAssignmentById(IDAssignment);
                if(assignmentDTO != null)
                {
                    IDPeriod = assignmentDTO.IDPeriod;
                    IDStage = assignmentDTO.IDStage;
                    IDSubStage = assignmentDTO.IDSubStage;
                }
            }            

            // Get Candidate
            CandidateDTO candidateDTO = candidateRepository.FindAll().Where(x => x.IDCandidate.Equals(IDCandidate)).FirstOrDefault();

            // Get Question
            List<ScoringComponentDTO> questionDTOs = scoringComponentRepository.GetScoringComponentListByStage(IDPeriod, IDStage, IDSubStage, candidateDTO.IDPosition).ToList();
            List<Guid> IDQuestionList = questionDTOs.Select(x => x.IDScoringComponent).ToList();
            List<Guid> IDQuestionTypeList = questionDTOs.Select(x => x.IDScoringComponentType).ToList();

            // Get Question Type
            List<ScoringComponentTypeDTO> questionTypeDTOs = scoringComponentTypeRepository.GetScoringComponentTypeByListID(IDQuestionTypeList).ToList();            

            // Get Candidate Score
            List<CandidateScoreDTO> candidateScoreDTOs = candidateScoreRepository.GetCandidateScoreListByQuestionID(IDQuestionList, IDCandidate).ToList();

            // Schedule
            //transactionScheduleRepository.Get

            foreach(var item in questionDTOs)
            {
                CandidateScoreDTO score = candidateScoreDTOs.Where(x => x.IDScoringComponent.Equals(item.IDScoringComponent)).FirstOrDefault();
                ScoringComponentTypeDTO questionType = questionTypeDTOs.Where(x => x.IDScoringComponentType.Equals(item.IDScoringComponentType)).FirstOrDefault();

                ScoringComponent question = new ScoringComponent()
                {
                    IDScoringComponent = item.IDScoringComponent,
                    IDScoringComponentType = item.IDScoringComponentType,
                    ScoringComponentText = item.ScoringComponent,
                    ScoringComponentType = questionType != null ? questionType.ScoringComponentType : "",
                    MinScore = item.MinScore,
                    MaxScore = item.MaxScore,
                    Notes = score != null ? score.Note : null,
                    Score = score != null ? score.Score : 0
                };

                result.Add(question);
            }

            result = result.OrderBy(x => x.IDScoringComponentType).ThenBy(x => x.MaxScore).ToList(); 

            return result;
        }

        public bool SaveCandidateScore(List<ScoringComponent> QuestionList, Guid IDCandidate, Guid IDSchedule, Guid IDUser)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(candidateScoreRepository).ToUse(ctx);
                    r.ConvertContextOfRepository(masterScheduleRepository).ToUse(ctx);

                    List<Guid> IDQuestionList = QuestionList.Select(x => x.IDScoringComponent).ToList();
                    List<CandidateScoreDTO> candidateScoreDTOs = candidateScoreRepository.GetCandidateScoreListByQuestionID(IDQuestionList, IDCandidate).ToList();

                    // Get Schedule
                    MasterScheduleDTO scheduleDTO = masterScheduleRepository.GetScheduleByIDSchedule(IDSchedule);
                    if(scheduleDTO.IDReviewer == null || !scheduleDTO.IDReviewer.Equals(IDUser))
                    {
                        // Update Reviewer
                        scheduleDTO.IDReviewer = IDUser;
                        masterScheduleRepository.Update(scheduleDTO);
                    }

                    foreach (ScoringComponent question in QuestionList)
                    {
                        CandidateScoreDTO score = candidateScoreDTOs.Where(x => x.IDScoringComponent.Equals(question.IDScoringComponent) && x.IDCandidate.Equals(IDCandidate)).FirstOrDefault();
                        if(score != null)
                        {
                            // UPDATE
                            score.Score = question.Score;
                            score.Note = question.Notes;

                            candidateScoreRepository.Update(score);
                        }
                        else
                        {
                            // INSERT
                            CandidateScoreDTO insert = new CandidateScoreDTO()
                            {
                                IDCandidate = IDCandidate,
                                IDScoringComponent = question.IDScoringComponent,
                                Score = question.Score,
                                Note = question.Notes
                            };

                            candidateScoreRepository.Insert(insert);
                        }
                    }
                });

                return true;
            }
            catch
            {
                return false;
            }            
        }
    }
}
