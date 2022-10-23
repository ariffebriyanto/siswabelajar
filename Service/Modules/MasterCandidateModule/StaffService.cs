using Helper;
using Helper.RandomHelper;
using Model.DTO.OneStopRecruitmentDTO;
using Model.DTO.OracleDTO;
using Model.Subdomains.EmailSubdomain;
using Model.Subdomains.MasterCandidateSubdomain;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using Repository.Repositories.OracleRepository;
using Service.Helpers.EmailHelper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.MasterCandidateModule
{
    public interface IStaffService
    {
        List<Period> GetAllPeriod();
        List<Stage> GetStages();
        List<Position> GetPositions();
        List<Candidate> GetScreeningTestCandidate(int IDPeriod, string IDPosition);
        List<Candidate> GetLogicTestCandidate(int IDPeriod, string IDPosition);
        List<Candidate> GetCandidateScoreWithSubStage(int IDPeriod, int IDStage, string IDPosition);
        List<Candidate> GetCandidateScoreWithoutSubStage(int IDPeriod, int IDStage, string IDPosition);
        bool SubmitCandidate(List<Candidate> CandidateList, int IDNextStage);
        bool UpdateToNextStage(int IDNextStage);
        bool SaveCandidateDraft(List<Candidate> CandidateList);
        bool GenereateCandidateUser(List<Candidate> CandidateList);
        bool CanSubmitCandidate(int IDPeriod, int IDStage);
        List<SubStage> GetSubStageBySchedule(int IDPeriod, int IDStage, string IDPosition);

        bool BlastCandidateStatus(List<Candidate> CandidateList, int IDPeriod, int IDStage);
    }
    public class StaffService : IStaffService
    {
        private readonly ICandidateRepository candidateRepository;
        private readonly IPS_N_PERSONCAR_TBLRepository personCarRepository;
        private readonly IMinimumScoreRepository minimumScoreRepository;
        private readonly IPeriodRepository periodRepository;
        private readonly ILogicTestAnswerRepository logicTestAnswerRepository;
        private readonly IUserRepository userRepository;
        private readonly ISubStageRepository subStageRepository;
        private readonly ICandidateScoreRepository candidateScoreRepository;
        private readonly IScoringComponentRepository scoringComponentRepository;
        private readonly IMasterScheduleRepository masterScheduleRepository;
        private readonly IStageRepository stageRepository;
        private readonly IPositionRepository positionRepository;
        private readonly ICandidateDraftRepository candidateDraftRepository;
        private readonly IMasterLogicTestQuestionRepository masterLogicTestQuestionRepository;
        private readonly IEmailTemplateRepository emailTemplateRepository;
        private readonly IEmailHelper emailHelper;        
        private readonly UnitOfWork unitOfWork;

        public StaffService(ICandidateRepository candidateRepository,
            IPS_N_PERSONCAR_TBLRepository personCarRepository,
            IMinimumScoreRepository minimumScoreRepository,
            IPeriodRepository periodRepository,
            ILogicTestAnswerRepository logicTestAnswerRepository,
            IUserRepository userRepository,
            ISubStageRepository subStageRepository,
            ICandidateScoreRepository candidateScoreRepository,
            IScoringComponentRepository scoringComponentRepository,
            IMasterScheduleRepository masterScheduleRepository,
            IStageRepository stageRepository,
            IPositionRepository positionRepository,
            ICandidateDraftRepository candidateDraftRepository,
            IMasterLogicTestQuestionRepository masterLogicTestQuestionRepository,
            IEmailTemplateRepository emailTemplateRepository,
            IEmailHelper emailHelper,
            UnitOfWork unitOfWork)
        {
            this.candidateRepository = candidateRepository;
            this.personCarRepository = personCarRepository;
            this.minimumScoreRepository = minimumScoreRepository;
            this.periodRepository = periodRepository;
            this.logicTestAnswerRepository = logicTestAnswerRepository;
            this.userRepository = userRepository;
            this.subStageRepository = subStageRepository;
            this.candidateScoreRepository = candidateScoreRepository;
            this.scoringComponentRepository = scoringComponentRepository;
            this.masterScheduleRepository = masterScheduleRepository;
            this.stageRepository = stageRepository;
            this.positionRepository = positionRepository;
            this.candidateDraftRepository = candidateDraftRepository;
            this.masterLogicTestQuestionRepository = masterLogicTestQuestionRepository;
            this.emailTemplateRepository = emailTemplateRepository;
            this.emailHelper = emailHelper;
            this.unitOfWork = unitOfWork;
        }

        public List<Period> GetAllPeriod()
        {
            List<PeriodDTO> periodList = periodRepository.FindAll().ToList();

            List<Period> result = new List<Period>();
            foreach (PeriodDTO item in periodList)
            {
                Period period = new Period();
                period.IDPeriod = item.IDPeriod;
                period.IDStage = item.IDStage;
                period.PeriodName = item.PeriodName;
                period.IsActive = item.IsActive;
                period.DeadlineStart = item.DeadlineStart;
                period.DeadlineEnd = item.DeadlineEnd;
                period.IsComplete = item.IsComplete;

                result.Add(period);
            }

            return result;
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
                    StageName = item.StageName,
                    StageLevel = item.StageLevel
                });
            }
            return stages;
        }

        public List<Position> GetPositions()
        {
            List<Position> positions = new List<Position>();
            var allPosition = positionRepository.FindAll().ToList();
            foreach (var item in allPosition)
            {
                Position position = new Position();
                position.IDPosition = item.IDPosition;
                position.PositionName = item.PositionName;
                positions.Add(position);
            }
            return positions;
        }

        public List<Candidate> GetScreeningTestCandidate(int IDPeriod, string IDPosition)
        {
            List<CandidateDTO> candidateList = candidateRepository.GetCandidateByStage(IDPeriod, 1, IDPosition).ToList();
            MinimumScoreDTO minimumScore = minimumScoreRepository.CheckMinimumScore(IDPeriod, 1, 0);
            List<CandidateDraftDTO> candidateDraftList = candidateDraftRepository.GetCandidateDraftByPeriodStagePosition(IDPeriod, 1, IDPosition).ToList();
            List<Candidate> result = new List<Candidate>();
            PeriodDTO currentPeriod = periodRepository.GetActivePeriod();
            List<string> candidateEmplidList = candidateList.Select(x => x.NIM).Distinct().ToList();
            List<PS_N_PERSONCAR_TBLDTO> candidateDetailsList = personCarRepository.GetUserByUserInputList(candidateEmplidList).ToList();

            foreach(var candidate in candidateList)
            {
               PS_N_PERSONCAR_TBLDTO personCar = candidateDetailsList
                                                    .Where(x => x.EMPLID.Equals(candidate.NIM) ||
                                                                x.EXTERNAL_SYSTEM_ID.Equals(candidate.NIM) ||
                                                                x.N_STDNT_ID2.Equals(candidate.NIM)
                                                    ).FirstOrDefault();

                Candidate item = new Candidate()
                {
                    IDPeriod = IDPeriod,
                    IDPosition = IDPosition,
                    IDRole = candidate.IDRole,
                    IDCandidate = candidate.IDCandidate,
                    IDStage = candidate.IDStage,
                    NIM = candidate.NIM,
                    Email = candidate.Email,
                    GPA = personCar == null ? 0 : personCar.CUM_GPA,
                    Name = personCar == null ? "" : personCar.FIRST_NAME + " " + personCar.LAST_NAME,
                    IsPass = (minimumScore == null || personCar == null) ? false : personCar.CUM_GPA >= minimumScore.MinimumScore,
                    IsMeetCriteria = (minimumScore == null || personCar == null) ? false : personCar.CUM_GPA >= minimumScore.MinimumScore
                };

                // Get From Draft
                CandidateDraftDTO draft = candidateDraftList.Where(x => x.IDCandidate.Equals(candidate.IDCandidate)).FirstOrDefault();
                if (draft != null)
                {
                    item.Note = draft.Note;
                }

                // Already Submitted To Next Stage
                if (candidate.IDStage > currentPeriod.IDStage)
                {
                    item.IsPass = true;
                    item.IsAlreadySubmit = true;
                }

                // This Stage Already Pass
                if (currentPeriod.IDStage > 1)
                {
                    item.IsPass = candidate.IDStage > 1 ? true : false;
                }
                else
                {
                    // Stage Hasn't Pass, Check IsPass from Draft
                    if (draft != null)
                    {
                        item.IsPass = draft.IsPass == 1 ? true : false;
                    }
                }                                

                result.Add(item);
            }

            // TODO: order by score and ispass
            result = result.OrderBy(x => x.IsPass).ThenBy(x => x.GPA).Reverse().ToList();

            return result;
        }


        public List<Candidate> GetLogicTestCandidate(int IDPeriod, string IDPosition)
        {
            List<CandidateDTO> candidateList = candidateRepository.GetCandidateByStage(IDPeriod, 2, IDPosition).ToList();
            List<CandidateDraftDTO> candidateDraftList = candidateDraftRepository.GetCandidateDraftByPeriodStagePosition(IDPeriod, 2, IDPosition).ToList();
            MinimumScoreDTO minimumScore = minimumScoreRepository.CheckMinimumScore(IDPeriod, 2, 0);
            List<Candidate> result = new List<Candidate>();
            PeriodDTO currentPeriod = periodRepository.GetActivePeriod();
            List<string> candidateEmplidList = candidateList.Select(x => x.NIM).Distinct().ToList();
            List<PS_N_PERSONCAR_TBLDTO> candidateDetailsList = personCarRepository.GetUserByUserInputList(candidateEmplidList).ToList();
            List<Guid> candidateIdList = candidateList.Select(x => x.IDCandidate).Distinct().ToList();

            List<LogicTestAnswerDTO> logicTestScoreList = logicTestAnswerRepository.GetCandidateLogicTestScoreByCandidateIdList(candidateIdList, IDPeriod).ToList();
            List<Guid> IDLogicTestQuestionList = logicTestScoreList.Select(x => x.IDLogicTestQuestion).Distinct().ToList();
            List<MasterLogicTestQuestionDTO> questionList = masterLogicTestQuestionRepository.GetQuestionsByIDQuestionList(IDLogicTestQuestionList).ToList();

            foreach (var candidate in candidateList)
            {
                PS_N_PERSONCAR_TBLDTO personCar = candidateDetailsList
                                                    .Where(x => x.EXTERNAL_SYSTEM_ID.Equals(candidate.NIM) ||
                                                                x.N_STDNT_ID2.Equals(candidate.NIM)
                                                    ).FirstOrDefault();

                //LogicTestAnswerDTO logicTestScoreDTO = logicTestScoreList.Where(x => x.IDCandidate.Equals(candidate.IDCandidate)).FirstOrDefault();
                List<LogicTestAnswerDTO> candidateAnswer = logicTestScoreList.Where(x => x.IDCandidate.Equals(candidate.IDCandidate)).ToList();
                int score = 0;
                foreach (var answer in candidateAnswer)
                {
                    MasterLogicTestQuestionDTO question = questionList.Where(x => x.IDLogicTestQuestion.Equals(answer.IDLogicTestQuestion)).FirstOrDefault();
                    if (question.CorrectChoice.Trim().ToLower().Equals(answer.Answer.Trim().ToLower()))
                    {
                        score += 4;
                    }
                }

                Candidate item = new Candidate()
                {
                    IDPeriod = IDPeriod,
                    IDPosition = IDPosition,
                    IDRole = candidate.IDRole,
                    IDCandidate = candidate.IDCandidate,
                    IDStage = candidate.IDStage,
                    NIM = candidate.NIM,
                    Email = candidate.Email,
                    GPA = personCar == null ? 0 : personCar.CUM_GPA,
                    Name = personCar == null ? "" : personCar.FIRST_NAME + " " + personCar.LAST_NAME,
                    LogicTestScore = score,
                    IsPass = minimumScore == null ? false : score >= minimumScore.MinimumScore,
                    IsMeetCriteria = minimumScore == null ? false : score >= minimumScore.MinimumScore
                };

                // Get From Draft
                CandidateDraftDTO draft = candidateDraftList.Where(x => x.IDCandidate.Equals(candidate.IDCandidate)).FirstOrDefault();
                if (draft != null)
                {
                    item.Note = draft.Note;
                }

                // Already Submitted To Next Stage
                if (candidate.IDStage > currentPeriod.IDStage)
                {
                    item.IsPass = true;
                    item.IsAlreadySubmit = true;
                }

                // This Stage Already Pass
                if (currentPeriod.IDStage > 2)
                {
                    item.IsPass = candidate.IDStage > 2 ? true : false;
                }
                else
                {
                    // Stage Hasn't Pass, Check IsPass from Draft
                    if (draft != null)
                    {
                        item.IsPass = draft.IsPass == 1 ? true : false;
                    }
                }

                result.Add(item);
            }

            // TODO: order by score and ispass
            result = result.OrderBy(x => x.IsPass).ThenBy(x => x.LogicTestScore).Reverse().ToList();

            return result;
        }

        public List<Candidate> GetCandidateScoreWithSubStage(int IDPeriod, int IDStage, string IDPosition)
        {
            List<Candidate> result = new List<Candidate>();
            List<CandidateDTO> candidateList = candidateRepository.GetCandidateByStage(IDPeriod, IDStage, IDPosition).ToList();
            List<CandidateDraftDTO> candidateDraftList = candidateDraftRepository.GetCandidateDraftByPeriodStagePosition(IDPeriod, IDStage, IDPosition).ToList();
            List<MasterScheduleDTO> scheduleList = masterScheduleRepository.GetScheduleByPeriodStagePosition(IDPeriod, IDStage, IDPosition).ToList();
            List<int> idSubStageList  = scheduleList.OrderBy(x => x.IDSubStage).Select(x => x.IDSubStage).Distinct().ToList();
            List<SubStageDTO> subStageList = subStageRepository.GetSubStagesByIDSubStageList(idSubStageList).ToList();
            List<string> candidateEmplidList = candidateList.Select(x => x.NIM).Distinct().ToList();
            List<PS_N_PERSONCAR_TBLDTO> candidateDetailsList = personCarRepository.GetUserByUserInputList(candidateEmplidList).ToList();
            List<Guid> candidateIdList = candidateList.Select(x => x.IDCandidate).Distinct().ToList();
            List<int> substageIdList = subStageList.Select(x => x.IDSubStage).Distinct().ToList();
            List<MinimumScoreDTO> minimumScoreList = minimumScoreRepository.CheckMinimumScoreBySubStageList(IDPeriod, IDStage, substageIdList).ToList();
            List<ScoringComponentDTO> questionBySubStageList = scoringComponentRepository.GetScoringComponentListByStageAndSubstageIdList(IDPeriod, IDStage, substageIdList, IDPosition).ToList();
            List<CandidateScoreDTO> candidateScoreByCandidateIdList = candidateScoreRepository.GetCandidateScoreListByQuestionIDAndCandidateIdList(questionBySubStageList ?? new List<ScoringComponentDTO>(), candidateIdList).ToList();
            PeriodDTO currentPeriod = periodRepository.GetActivePeriod();

            foreach(var candidate in candidateList)
            {
                PS_N_PERSONCAR_TBLDTO personCar = candidateDetailsList
                                                    .Where(x => x.EXTERNAL_SYSTEM_ID.Equals(candidate.NIM) ||
                                                                x.N_STDNT_ID2.Equals(candidate.NIM)
                                                    ).FirstOrDefault();
                List<SubStage> subStageScoreList = new List<SubStage>();
                bool IsPass = true;
                foreach (var subStage in subStageList)
                {
                    MinimumScoreDTO minimumScoreDTO = minimumScoreList.Where(x => x.IDSubStage == subStage.IDSubStage).FirstOrDefault();
                    List<ScoringComponentDTO> questionList = questionBySubStageList.Where(x => x.IDSubStage == subStage.IDSubStage).ToList();
                    List<Guid> questionIdList = questionList.Select(x => x.IDScoringComponent).Distinct().ToList();
                    List<CandidateScoreDTO> candidateScoreList = candidateScoreByCandidateIdList
                                                                        .Where(x => questionIdList.Contains(x.IDScoringComponent) && 
                                                                                    x.IDCandidate.Equals(candidate.IDCandidate))
                                                                        .ToList();

                    decimal score = candidateScoreList == null ? 0 : candidateScoreList.Sum(x => x.Score);
                    decimal minimalScore = minimumScoreDTO == null ? 0 : minimumScoreDTO.MinimumScore;

                    subStageScoreList.Add(new SubStage()
                    {
                        IDStage = IDStage,
                        IDSubStage = subStage.IDSubStage,
                        Score = score,
                        MinimumScore = minimalScore,
                        SubStageName = subStage.SubStageName
                    });

                    if(candidate.IDStage <= IDStage)
                    {
                        if (score < minimalScore)
                        {
                            IsPass = false;
                        }
                    }                    
                }
                
                Candidate item = new Candidate()
                {
                    IDPeriod = IDPeriod,
                    IDPosition = IDPosition,
                    IDRole = candidate.IDRole,
                    IDCandidate = candidate.IDCandidate,
                    IDStage = candidate.IDStage,
                    NIM = candidate.NIM,
                    Email = candidate.Email,
                    GPA = personCar == null ? 0 : personCar.CUM_GPA,
                    Name = personCar == null ? "" : personCar.FIRST_NAME + " " + personCar.LAST_NAME,
                    SubStageScoreList = subStageScoreList,
                    IsPass = IsPass,
                    IsMeetCriteria = IsPass
                };

                // Get From Draft
                CandidateDraftDTO draft = candidateDraftList.Where(x => x.IDCandidate.Equals(candidate.IDCandidate)).FirstOrDefault();
                if (draft != null)
                {
                    item.Note = draft.Note;
                }

                // Already Submitted To Next Stage
                if (candidate.IDStage > currentPeriod.IDStage)
                {
                    item.IsPass = true;
                    item.IsAlreadySubmit = true;
                }

                // This Stage Already Pass
                if (currentPeriod.IDStage > IDStage || currentPeriod.IsComplete == 1)
                {
                    item.IsPass = candidate.IDStage > IDStage || candidate.IsAccepted == 1 ? true : false;
                }
                else
                {
                    // Stage Hasn't Pass, Check IsPass from Draft
                    if (draft != null)
                    {
                        item.IsPass = draft.IsPass == 1 ? true : false;
                    }
                }
                
                result.Add(item);
            }

            result = result.OrderBy(x => x.IsPass).Reverse().ToList();

            return result;
        }

        public List<Candidate> GetCandidateScoreWithoutSubStage(int IDPeriod, int IDStage, string IDPosition)
        {
            List<Candidate> result = new List<Candidate>();
            List<CandidateDTO> candidateList = candidateRepository.GetCandidateByStage(IDPeriod, IDStage, IDPosition).ToList();
            List<CandidateDraftDTO> candidateDraftList = candidateDraftRepository.GetCandidateDraftByPeriodStagePosition(IDPeriod, IDStage, IDPosition).ToList();
            List<string> candidateEmplidList = candidateList.Select(x => x.NIM).Distinct().ToList();
            List<PS_N_PERSONCAR_TBLDTO> candidateDetailsList = personCarRepository.GetUserByUserInputList(candidateEmplidList).ToList();
            List<Guid> candidateIdList = candidateList.Select(x => x.IDCandidate).Distinct().ToList();
            MinimumScoreDTO minimumScoreDTO = minimumScoreRepository.CheckMinimumScore(IDPeriod, IDStage, 0);
            List<ScoringComponentDTO> questionList = scoringComponentRepository.GetScoringComponentListByStage(IDPeriod, IDStage, 0, IDPosition).ToList();
            List<CandidateScoreDTO> candidateScoreByCandidateIdList = candidateScoreRepository.GetCandidateScoreListByQuestionIDAndCandidateIdList(questionList ?? new List<ScoringComponentDTO>(), candidateIdList).ToList();
            PeriodDTO currentPeriod = periodRepository.GetActivePeriod();

            foreach (var candidate in candidateList)
            {
                PS_N_PERSONCAR_TBLDTO personCar = candidateDetailsList
                                                    .Where(x => x.EXTERNAL_SYSTEM_ID.Equals(candidate.NIM) ||
                                                                x.N_STDNT_ID2.Equals(candidate.NIM)
                                                    ).FirstOrDefault();
                List<CandidateScoreDTO> candidateScoreList = candidateScoreByCandidateIdList.Where(x => x.IDCandidate.Equals(candidate.IDCandidate)).ToList();

                decimal score = candidateScoreList == null ? 0 : candidateScoreList.Sum(x => x.Score);
                decimal minimalScore = minimumScoreDTO == null ? 0 : minimumScoreDTO.MinimumScore;

                Candidate item = new Candidate()
                {
                    IDPeriod = IDPeriod,
                    IDPosition = IDPosition,
                    IDRole = candidate.IDRole,
                    IDCandidate = candidate.IDCandidate,
                    IDStage = candidate.IDStage,
                    NIM = candidate.NIM,
                    Email = candidate.Email,
                    GPA = personCar == null ? 0 : personCar.CUM_GPA,
                    Name = personCar == null ? "" : personCar.FIRST_NAME + " " + personCar.LAST_NAME,
                    Score = score,
                    IsPass = score >= minimalScore ? true : false,
                    IsMeetCriteria = score >= minimalScore ? true : false
                };

                // Get From Draft
                CandidateDraftDTO draft = candidateDraftList.Where(x => x.IDCandidate.Equals(candidate.IDCandidate)).FirstOrDefault();
                if (draft != null)
                {
                    item.Note = draft.Note;
                }

                // Already Submitted To Next Stage
                if (candidate.IDStage > currentPeriod.IDStage)
                {
                    item.IsPass = true;
                    item.IsAlreadySubmit = true;
                }

                // This Stage Already Pass
                if (currentPeriod.IDStage > IDStage)
                {
                    item.IsPass = candidate.IDStage > IDStage ? true : false;
                }
                else
                {
                    // Stage Hasn't Pass, Check IsPass from Draft
                    if (draft != null)
                    {
                        item.IsPass = draft.IsPass == 1 ? true : false;
                    }
                }                

                result.Add(item);
            }

            result = result.OrderBy(x => x.IsPass).Reverse().ToList();

            return result;
        }

        public bool SubmitCandidate(List<Candidate> CandidateList, int IDStage)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(candidateRepository).ToUse(ctx);
                    r.ConvertContextOfRepository(periodRepository).ToUse(ctx);
                    r.ConvertContextOfRepository(candidateDraftRepository).ToUse(ctx);

                    if (CandidateList != null && CandidateList.Count > 0)
                    {
                        StageDTO maxStage = stageRepository.GetLastStage();
                        List<Guid> candidateIdList = CandidateList.Select(x => x.IDCandidate).Distinct().ToList();
                        List<CandidateDTO> candidateDetailList = candidateRepository.GetCandidateByCandidateIdList(candidateIdList).ToList();
                        List<CandidateDraftDTO> existstingDraftList = candidateDraftRepository
                                                            .GetCandidateDraftByPeriodStagePosition(CandidateList[0].IDPeriod,
                                                                                                    CandidateList[0].IDStage,
                                                                                                    CandidateList[0].IDPosition).ToList();

                        foreach (var item in CandidateList)
                        {
                            if (item.IsPass)
                            {
                                CandidateDTO candidate = candidateDetailList.Where(x => x.IDCandidate.Equals(item.IDCandidate)).FirstOrDefault();

                                if (maxStage.IDStage == IDStage)
                                {
                                    candidate.IsAccepted = 1;
                                }
                                else
                                {
                                    candidate.IDStage = IDStage + 1;
                                }
                                candidateRepository.Update(candidate);
                            }

                            if (item.Note != null)
                            {
                                // Save Notes
                                CandidateDraftDTO existstingDraft = existstingDraftList.Where(x => x.IDCandidate.Equals(item.IDCandidate)).FirstOrDefault();
                                if (existstingDraft != null)
                                {
                                    // Update
                                    existstingDraft.IsPass = item.IsPass ? 1 : 0;
                                    existstingDraft.Note = item.Note;
                                    candidateDraftRepository.Update(existstingDraft);
                                }
                                else
                                {
                                    // Insert
                                    CandidateDraftDTO draft = new CandidateDraftDTO()
                                    {
                                        IDCandidate = item.IDCandidate,
                                        IDPeriod = item.IDPeriod,
                                        IDStage = item.IDStage,
                                        IDPosition = item.IDPosition,
                                        IsPass = item.IsPass ? 1 : 0,
                                        Note = item.Note
                                    };
                                    candidateDraftRepository.Insert(draft);
                                }
                            }
                        }

                        //// Update Current Period's Stage                    
                        //PeriodDTO period = periodRepository.GetActivePeriod();
                        //if (maxStage.IDStage == IDStage)
                        //{
                        //    period.IsComplete = 1;
                        //}
                        //else
                        //{
                        //    period.IDStage = IDStage + 1;
                        //}
                        //periodRepository.Update(period);
                    }                    
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateToNextStage(int IDStage)
        {
            try
            {
                StageDTO maxStage = stageRepository.GetLastStage();   
                PeriodDTO period = periodRepository.GetActivePeriod();
                if (maxStage.IDStage == IDStage)
                {
                    period.IsComplete = 1;
                }
                else
                {
                    period.IDStage = IDStage + 1;
                }
                periodRepository.Update(period);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveCandidateDraft(List<Candidate> CandidateList)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(candidateDraftRepository).ToUse(ctx);                    

                    if(CandidateList != null && CandidateList.Count > 0)
                    {
                        List<CandidateDraftDTO> draftList = new List<CandidateDraftDTO>();
                        List<CandidateDraftDTO> existstingDraftList = candidateDraftRepository
                                                        .GetCandidateDraftByPeriodStagePosition(CandidateList[0].IDPeriod,
                                                                                                CandidateList[0].IDStage,
                                                                                                CandidateList[0].IDPosition).ToList();


                        foreach (var item in CandidateList)
                        {
                            CandidateDraftDTO existstingDraft = existstingDraftList.Where(x => x.IDCandidate.Equals(item.IDCandidate)).FirstOrDefault();
                            if(existstingDraft != null)
                            {
                                // Update
                                existstingDraft.IsPass = item.IsPass ? 1 : 0;
                                existstingDraft.Note = item.Note;
                                candidateDraftRepository.Update(existstingDraft);
                            }
                            else
                            {
                                // Insert
                                CandidateDraftDTO draft = new CandidateDraftDTO()
                                {
                                    IDCandidate = item.IDCandidate,
                                    IDPeriod = item.IDPeriod,
                                    IDStage = item.IDStage,
                                    IDPosition = item.IDPosition,
                                    IsPass = item.IsPass ? 1 : 0,
                                    Note = item.Note
                                };
                                candidateDraftRepository.Insert(draft);
                            }                            
                        }
                    }                    
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GenereateCandidateUser(List<Candidate> CandidateList)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(userRepository).ToUse(ctx);

                    List<string> candidateNIMList = CandidateList.Select(x => x.NIM).Distinct().ToList();
                    List<UserDTO> searchUserList = userRepository.GetUserByUsernameList(candidateNIMList).ToList();
                    List<PS_N_PERSONCAR_TBLDTO> studentList = personCarRepository.GetUserByUserInputList(candidateNIMList).ToList();
                    foreach (var item in CandidateList)
                    {
                        if (item.IsPass)
                        {
                            PS_N_PERSONCAR_TBLDTO student = studentList.Where(x => x.EXTERNAL_SYSTEM_ID.Equals(item.NIM)).FirstOrDefault();
                            item.Password = "uni" + student.BIRTHDATE.ToString("ddMMyyyy");
                            UserDTO searchUser = searchUserList.Where(x => x.Username.ToLower().Equals(item.NIM.ToLower())).FirstOrDefault();

                            if (searchUser != null)
                            {
                                searchUser.Password = SHA256Encryption.Encrypt(item.Password);
                                userRepository.Update(searchUser);
                            }
                            else
                            {
                                UserDTO user = new UserDTO()
                                {
                                    IDRole = 2,
                                    Email = item.Email,
                                    Name = item.Name,
                                    Username = item.NIM,
                                    Password = SHA256Encryption.Encrypt(item.Password)
                                };
                                userRepository.Insert(user);
                            }
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

        public bool CanSubmitCandidate(int IDPeriod, int IDStage)
        {
            try
            {
                PeriodDTO period = periodRepository.GetPeriodByID(IDPeriod);
                if (period.IsActive == 1 && period.IDStage == IDStage && period.IsComplete == 0)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public List<SubStage> GetSubStageBySchedule(int IDPeriod, int IDStage, string IDPosition)
        {
            try
            {
                List<MasterScheduleDTO> scheduleList = masterScheduleRepository.GetScheduleByPeriodStagePosition(IDPeriod, IDStage, IDPosition).ToList();
                List<SubStage> subStageList = new List<SubStage>();
                List<int> idSubStageList = scheduleList.OrderBy(x => x.IDSubStage).Select(x => x.IDSubStage).Distinct().ToList();
                List<SubStageDTO> subStageByListId = subStageRepository.GetSubStagesByIDSubStageList(idSubStageList).ToList();
                foreach (var idSubStage in idSubStageList)
                {
                    if (idSubStage == 0)
                    {
                        continue;
                    }

                    SubStageDTO dto = subStageByListId.Where(x => x.IDSubStage == idSubStage).FirstOrDefault();
                    if (dto == null)
                    {
                        continue;
                    }

                    subStageList.Add(new SubStage()
                    {
                        IDStage = dto.IDStage,
                        IDSubStage = dto.IDSubStage,
                        SubStageName = dto.SubStageName
                    });
                }
                return subStageList;
            }
            catch
            {
                return new List<SubStage>();
            }
        }

        public bool BlastCandidateStatus(List<Candidate> CandidateList, int IDPeriod, int IDStage)
        {
            try
            {
                // Split Accepted and Rejected Candidate
                List<string> accepted = new List<string>();
                List<string> rejected = new List<string>();

                foreach(Candidate candidate in CandidateList)
                {
                    if (candidate.IsPass)
                    {
                        accepted.Add(candidate.Email);
                    }
                    else
                    {
                        rejected.Add(candidate.Email);
                    }
                }

                // Get Accepted and Rejected Template 
                EmailTemplateDTO acceptedTemplate = emailTemplateRepository.FindAll()
                                                                    .Where(x => x.IDPeriod == IDPeriod && x.IDStage == IDStage)
                                                                    .FirstOrDefault();
                EmailTemplateDTO rejectedTemplate = emailTemplateRepository.FindAll()
                                                                    .Where(x => x.IDPeriod == 0 && x.IDStage == 0)
                                                                    .FirstOrDefault();


                // Blast Accepted and Rejected
                emailHelper.Send(new Email
                {
                    Recipients = accepted.Distinct().ToList(),
                    Subject = "IT Division Recruitment",
                    Body = acceptedTemplate.Template,
                    IsBodyHtml = true
                });

                emailHelper.Send(new Email
                {
                    Recipients = rejected.Distinct().ToList(),
                    Subject = "IT Division Recruitment",
                    Body = rejectedTemplate.Template,
                    IsBodyHtml = true
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
