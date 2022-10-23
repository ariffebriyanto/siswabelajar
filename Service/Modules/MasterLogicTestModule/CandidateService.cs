using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.MasterLogicTestSubdomain.Candidate;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.MasterLogicTestModule
{
    public interface ICandidateService
    {
        List<MasterLogicTestQuestion> GetLogicTestByPeriod(string Username);
        bool InsertUpdateLogicTestAnswer(Guid IDLogicTestQuestion, string Username, string Answer);
        MasterSchedule GetScheduleByID(Guid IDSchedule);
    }
    public class CandidateService : ICandidateService
    {
        private readonly IMasterLogicTestQuestionRepository masterLogicTestQuestionRepository;
        private readonly ITransactionLogicTestQuestionRepository transactionLogicTestQuestionRepository;
        private readonly IPeriodRepository periodRepository;
        private readonly ILogicTestAnswerRepository logicTestAnswerRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IMasterScheduleRepository masterScheduleRepository;
        private readonly UnitOfWork unitOfWork;
        public CandidateService(IMasterLogicTestQuestionRepository masterLogicTestQuestionRepository,
            ITransactionLogicTestQuestionRepository transactionLogicTestQuestionRepository,
            IPeriodRepository periodRepository,
            ILogicTestAnswerRepository logicTestAnswerRepository,
            ICandidateRepository candidateRepository,
            IMasterScheduleRepository masterScheduleRepository,
            UnitOfWork unitOfWork)
        {
            this.masterLogicTestQuestionRepository = masterLogicTestQuestionRepository;
            this.transactionLogicTestQuestionRepository = transactionLogicTestQuestionRepository;
            this.periodRepository = periodRepository;
            this.logicTestAnswerRepository = logicTestAnswerRepository;
            this.candidateRepository = candidateRepository;
            this.masterScheduleRepository = masterScheduleRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<MasterLogicTestQuestion> GetLogicTestByPeriod(string Username)
        {            
            List<MasterLogicTestQuestion> result = new List<MasterLogicTestQuestion>();

            PeriodDTO period = periodRepository.GetActivePeriod();
            int IDPeriod = period != null ? period.IDPeriod : 0;
            List<TransactionLogicTestQuestionDTO> transaction = transactionLogicTestQuestionRepository.GetSelectedQuestionByPeriod(IDPeriod).ToList();
            List<Guid> IDQuestionList = transaction.Select(x => x.IDLogicTestQuestion).ToList();
            List<MasterLogicTestQuestionDTO> questionList = masterLogicTestQuestionRepository.GetQuestionsByIDQuestionList(IDQuestionList).ToList();

            // Get Previous Answer
            CandidateDTO candidate = candidateRepository.GetCandidateByNIM(Username);
            List<LogicTestAnswerDTO> answerList = logicTestAnswerRepository.GetCandidateLogicTestAnswerList(candidate.IDCandidate, IDPeriod).ToList();

            foreach (var item in questionList)
            {
                LogicTestAnswerDTO answer = answerList.Where(x => x.IDLogicTestQuestion.Equals(item.IDLogicTestQuestion)).FirstOrDefault();

                MasterLogicTestQuestion question = new MasterLogicTestQuestion()
                {
                    IDLogicTestQuestion = item.IDLogicTestQuestion,
                    IDLogicTestQuestionType = item.IDLogicTestQuestionType,

                    Type = item.QuestionImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                    QuestionText = item.QuestionText,
                    QuestionImage = item.QuestionImage,

                    FirstChoiceType = item.FirstChoiceImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                    FirstChoiceText = item.FirstChoiceText,
                    FirstChoiceImage = item.FirstChoiceImage,

                    SecondChoiceType = item.SecondChoiceImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                    SecondChoiceText = item.SecondChoiceText,
                    SecondChoiceImage = item.SecondChoiceImage,

                    ThirdChoiceType = item.ThirdChoiceImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                    ThirdChoiceText = item.ThirdChoiceText,
                    ThirdChoiceImage = item.ThirdChoiceImage,

                    FourthChoiceType = item.FourthChoiceImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                    FourthChoiceText = item.FourthChoiceText,
                    FourthChoiceImage = item.FourthChoiceImage,

                    Answer = answer != null ? answer.Answer : ""
                };                
                result.Add(question);
            }
            return result;
        }

        public bool InsertUpdateLogicTestAnswer(Guid IDLogicTestQuestion, string Username, string Answer)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(logicTestAnswerRepository).ToUse(ctx);
                    PeriodDTO period = periodRepository.GetActivePeriod();
                    int IDPeriod = period != null ? period.IDPeriod : 0;
                    CandidateDTO candidate = candidateRepository.GetCandidateByNIM(Username);
                    LogicTestAnswerDTO getAnswer = logicTestAnswerRepository.GetCandidateLogicTestAnswer(IDLogicTestQuestion, candidate.IDCandidate, IDPeriod);

                    if(getAnswer == null)
                    {
                        // Insert
                        LogicTestAnswerDTO answer = new LogicTestAnswerDTO()
                        {
                            IDCandidate = candidate.IDCandidate,
                            IDLogicTestQuestion = IDLogicTestQuestion,
                            IDPeriod = IDPeriod,
                            Answer = Answer
                        };
                        logicTestAnswerRepository.Insert(answer);
                    }
                    else
                    {
                        // Update
                        getAnswer.Answer = Answer;
                        logicTestAnswerRepository.Update(getAnswer);
                    }
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public MasterSchedule GetScheduleByID(Guid IDSchedule)
        {
            MasterScheduleDTO schedule = masterScheduleRepository.GetScheduleByIDSchedule(IDSchedule);
            return new MasterSchedule()
            {
                IDSchedule = schedule.IDSchedule,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                Date = schedule.Date
            };
        }
    }
}
