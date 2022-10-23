using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.MasterLogicTestSubdomain;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.MasterLogicTestModule
{
    public interface IStaffService
    {
        List<Period> GetAllPeriod();
        int SearchQuestionType(string QuestionType);
        bool InsertQuestionType(LogicTestQuestionType logicTestQuestionType);
        List<LogicTestQuestionType> GetAllLogicTestQuestionType();
        bool InsertQuestion(MasterLogicTestQuestion question);
        bool UpdateQuestion(MasterLogicTestQuestion question);
        List<MasterLogicTestQuestion> GetAllLogicTestQuestion();
        MasterLogicTestQuestion GetQuestionByID(Guid IDLogicTestQuestion);
        List<MasterLogicTestQuestion> GetPickQuestionList(int IDPeriod);
        bool CheckedQuestion(bool IsChecked, string IDLogicTestQuestion, int IDPeriod);
        bool DeleteQuestion(string IDLogicTestQuestion);
    }
    public class StaffService : IStaffService
    {
        private readonly IPeriodRepository periodRepository;
        private readonly ITransactionLogicTestQuestionRepository transactionLogicTestQuestionRepository;
        private readonly IMasterLogicTestQuestionRepository masterLogicTestQuestionRepository;
        private readonly ILogicTestQuestionTypeRepository logicTestQuestionTypeRepository;
        private readonly UnitOfWork unitOfWork;

        public StaffService(IPeriodRepository periodRepository, 
            ITransactionLogicTestQuestionRepository transactionLogicTestQuestionRepository,
            IMasterLogicTestQuestionRepository masterLogicTestQuestionRepository,
            ILogicTestQuestionTypeRepository logicTestQuestionTypeRepository,
            UnitOfWork unitOfWork
        )
        {
            this.periodRepository = periodRepository;
            this.transactionLogicTestQuestionRepository = transactionLogicTestQuestionRepository;
            this.masterLogicTestQuestionRepository = masterLogicTestQuestionRepository;
            this.logicTestQuestionTypeRepository = logicTestQuestionTypeRepository;
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

        public int SearchQuestionType(string QuestionType)
        {            
            try
            {
                LogicTestQuestionTypeDTO response = logicTestQuestionTypeRepository.SearchQuestionType(QuestionType);
                if(response != null && response.IDLogicTestQuestionType != 0)
                {
                    return 1;
                }
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public bool InsertQuestionType(LogicTestQuestionType logicTestQuestionType)
        {
            bool result = true;
            try
            {
                unitOfWork.Run((r, ctx) => 
                {
                    r.ConvertContextOfRepository(logicTestQuestionTypeRepository).ToUse(ctx);
                    LogicTestQuestionTypeDTO request = new LogicTestQuestionTypeDTO()
                    {
                        LogicTestQuestionType = logicTestQuestionType.QuestionType
                    };

                    logicTestQuestionTypeRepository.Insert(request);
                });                              
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public List<LogicTestQuestionType> GetAllLogicTestQuestionType()
        {
            List<LogicTestQuestionType> result = new List<LogicTestQuestionType>();
            List<LogicTestQuestionTypeDTO> dtos = logicTestQuestionTypeRepository.FindAll().ToList();

            foreach(var item in dtos)
            {
                result.Add(new LogicTestQuestionType()
                {
                    IDLogicTestQuestionType = item.IDLogicTestQuestionType,
                    QuestionType = item.LogicTestQuestionType
                });
            }
            return result;
        }

        public bool InsertQuestion(MasterLogicTestQuestion question)
        {
            bool result = true;
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(masterLogicTestQuestionRepository).ToUse(ctx);

                    MasterLogicTestQuestionDTO request = new MasterLogicTestQuestionDTO()
                    {
                        IDLogicTestQuestionType = question.IDLogicTestQuestionType,

                        QuestionText = question.Type.Equals(BaseConstraint.OptionType.Text) ? question.QuestionText : null,
                        QuestionImage = question.Type.Equals(BaseConstraint.OptionType.Image) ? question.QuestionImage : null,

                        FirstChoiceText = question.FirstChoiceType.Equals(BaseConstraint.OptionType.Text) ? question.FirstChoiceText : null,
                        FirstChoiceImage = question.FirstChoiceType.Equals(BaseConstraint.OptionType.Image) ? question.FirstChoiceImage : null,

                        SecondChoiceText = question.SecondChoiceType.Equals(BaseConstraint.OptionType.Text) ? question.SecondChoiceText : null,
                        SecondChoiceImage = question.SecondChoiceType.Equals(BaseConstraint.OptionType.Image) ? question.SecondChoiceImage : null,

                        ThirdChoiceText = question.ThirdChoiceType.Equals(BaseConstraint.OptionType.Text) ? question.ThirdChoiceText : null,
                        ThirdChoiceImage = question.ThirdChoiceType.Equals(BaseConstraint.OptionType.Image) ? question.ThirdChoiceImage : null,

                        FourthChoiceText = question.FourthChoiceType.Equals(BaseConstraint.OptionType.Text) ? question.FourthChoiceText : null,
                        FourthChoiceImage = question.FourthChoiceType.Equals(BaseConstraint.OptionType.Image) ? question.FourthChoiceImage : null,

                        CorrectChoice = question.CorrectChoice
                    };

                    masterLogicTestQuestionRepository.Insert(request);
                });                
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }        

        public bool UpdateQuestion(MasterLogicTestQuestion question)
        {
            bool result = true;
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(masterLogicTestQuestionRepository).ToUse(ctx);

                    MasterLogicTestQuestionDTO response = masterLogicTestQuestionRepository.GetQuestionByID(question.IDLogicTestQuestion);

                    response.IDLogicTestQuestionType = question.IDLogicTestQuestionType;

                    response.QuestionText = question.Type.Equals(BaseConstraint.OptionType.Text) ? question.QuestionText : null;
                    response.QuestionImage = question.Type.Equals(BaseConstraint.OptionType.Image) ? question.QuestionImage : null;

                    response.FirstChoiceText = question.FirstChoiceType.Equals(BaseConstraint.OptionType.Text) ? question.FirstChoiceText : null;
                    response.FirstChoiceImage = question.FirstChoiceType.Equals(BaseConstraint.OptionType.Image) ? question.FirstChoiceImage : null;

                    response.SecondChoiceText = question.SecondChoiceType.Equals(BaseConstraint.OptionType.Text) ? question.SecondChoiceText : null;
                    response.SecondChoiceImage = question.SecondChoiceType.Equals(BaseConstraint.OptionType.Image) ? question.SecondChoiceImage : null;

                    response.ThirdChoiceText = question.ThirdChoiceType.Equals(BaseConstraint.OptionType.Text) ? question.ThirdChoiceText : null;
                    response.ThirdChoiceImage = question.ThirdChoiceType.Equals(BaseConstraint.OptionType.Image) ? question.ThirdChoiceImage : null;

                    response.FourthChoiceText = question.FourthChoiceType.Equals(BaseConstraint.OptionType.Text) ? question.FourthChoiceText : null;
                    response.FourthChoiceImage = question.FourthChoiceType.Equals(BaseConstraint.OptionType.Image) ? question.FourthChoiceImage : null;

                    response.CorrectChoice = question.CorrectChoice;

                    masterLogicTestQuestionRepository.Update(response);
                });                
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public List<MasterLogicTestQuestion> GetAllLogicTestQuestion()
        {
            List<MasterLogicTestQuestion> result = new List<MasterLogicTestQuestion>();

            List<MasterLogicTestQuestionDTO> questionList = masterLogicTestQuestionRepository.FindAll().ToList();            
            foreach(var item in questionList)
            {
                LogicTestQuestionTypeDTO questionType = logicTestQuestionTypeRepository.GetQuestionTypeByID(item.IDLogicTestQuestionType);
                result.Add(new MasterLogicTestQuestion()
                {
                    IDLogicTestQuestion = item.IDLogicTestQuestion,
                    IDLogicTestQuestionType = item.IDLogicTestQuestionType,
                    QuestionType = questionType.LogicTestQuestionType,

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

                    CorrectChoice = item.CorrectChoice
                });
            }

            return result;
        }

        public MasterLogicTestQuestion GetQuestionByID(Guid IDLogicTestQuestion)
        {
            MasterLogicTestQuestionDTO response = masterLogicTestQuestionRepository.GetQuestionByID(IDLogicTestQuestion);
            return new MasterLogicTestQuestion()
            {
                IDLogicTestQuestion = response.IDLogicTestQuestion,
                IDLogicTestQuestionType = response.IDLogicTestQuestionType,

                Type = response.QuestionImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                QuestionText = response.QuestionText,
                QuestionImage = response.QuestionImage,

                FirstChoiceType = response.FirstChoiceImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                FirstChoiceText = response.FirstChoiceText,
                FirstChoiceImage = response.FirstChoiceImage,

                SecondChoiceType = response.SecondChoiceImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                SecondChoiceText = response.SecondChoiceText,
                SecondChoiceImage = response.SecondChoiceImage,

                ThirdChoiceType = response.ThirdChoiceImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                ThirdChoiceText = response.ThirdChoiceText,
                ThirdChoiceImage = response.ThirdChoiceImage,

                FourthChoiceType = response.FourthChoiceImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                FourthChoiceText = response.FourthChoiceText,
                FourthChoiceImage = response.FourthChoiceImage,

                CorrectChoice = response.CorrectChoice
            };
        }

        public List<MasterLogicTestQuestion> GetPickQuestionList(int IDPeriod)
        {
            List<MasterLogicTestQuestion> result = new List<MasterLogicTestQuestion>();
            List<MasterLogicTestQuestionDTO> questionList = masterLogicTestQuestionRepository.FindAll().ToList();
            List<TransactionLogicTestQuestionDTO> transactionList = transactionLogicTestQuestionRepository.GetSelectedQuestionByPeriod(IDPeriod).ToList();            

            foreach(MasterLogicTestQuestionDTO question in questionList)
            {
                LogicTestQuestionTypeDTO questionType = logicTestQuestionTypeRepository.GetQuestionTypeByID(question.IDLogicTestQuestionType);
                TransactionLogicTestQuestionDTO transaction = transactionList.Where(x => x.IDLogicTestQuestion == question.IDLogicTestQuestion).FirstOrDefault();
                bool IsPicked = false;

                if(transaction != null)
                {
                    IsPicked = true;
                }

                MasterLogicTestQuestion item = new MasterLogicTestQuestion()
                {
                    IDLogicTestQuestion = question.IDLogicTestQuestion,
                    IDLogicTestQuestionType = question.IDLogicTestQuestionType,
                    QuestionType = questionType.LogicTestQuestionType,

                    Type = question.QuestionImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                    QuestionText = question.QuestionText,
                    QuestionImage = question.QuestionImage,

                    FirstChoiceType = question.FirstChoiceImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                    FirstChoiceText = question.FirstChoiceText,
                    FirstChoiceImage = question.FirstChoiceImage,

                    SecondChoiceType = question.SecondChoiceImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                    SecondChoiceText = question.SecondChoiceText,
                    SecondChoiceImage = question.SecondChoiceImage,

                    ThirdChoiceType = question.ThirdChoiceImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                    ThirdChoiceText = question.ThirdChoiceText,
                    ThirdChoiceImage = question.ThirdChoiceImage,

                    FourthChoiceType = question.FourthChoiceImage == null ? BaseConstraint.OptionType.Text : BaseConstraint.OptionType.Image,
                    FourthChoiceText = question.FourthChoiceText,
                    FourthChoiceImage = question.FourthChoiceImage,

                    CorrectChoice = question.CorrectChoice,
                    IsPicked = IsPicked
                };
                result.Add(item);
            }

            result = result.OrderBy(x => !x.IsPicked).ToList();

            return result;
        }

        public bool CheckedQuestion(bool IsChecked, string IDLogicTestQuestion, int IDPeriod)
        {
            bool result = true;
            try
            {                
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(transactionLogicTestQuestionRepository).ToUse(ctx);

                    if (IsChecked)
                    {
                        TransactionLogicTestQuestionDTO request = new TransactionLogicTestQuestionDTO()
                        {
                            IDMappingQuestion = Guid.NewGuid(),
                            IDLogicTestQuestion = new Guid(IDLogicTestQuestion),
                            IDPeriod = IDPeriod
                        };
                        transactionLogicTestQuestionRepository.Insert(request);
                    }
                    else
                    {
                        TransactionLogicTestQuestionDTO request = transactionLogicTestQuestionRepository.GetTransactionByID(new Guid(IDLogicTestQuestion), IDPeriod);
                        transactionLogicTestQuestionRepository.Delete(request);
                    }
                });                                    
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool DeleteQuestion(string IDLogicTestQuestion)
        {
            bool result = true;
            try
            {
               
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(masterLogicTestQuestionRepository).ToUse(ctx);
                    r.ConvertContextOfRepository(transactionLogicTestQuestionRepository).ToUse(ctx);

                    Guid guid = new Guid(IDLogicTestQuestion);
                    MasterLogicTestQuestionDTO question = masterLogicTestQuestionRepository.GetQuestionByID(guid);
                    masterLogicTestQuestionRepository.Delete(question);

                    // Delete Picked Question
                    List<TransactionLogicTestQuestionDTO> transacionList = transactionLogicTestQuestionRepository.GetTransactionListByIDQuestion(guid).ToList();
                    foreach (TransactionLogicTestQuestionDTO item in transacionList)
                    {
                        transactionLogicTestQuestionRepository.Delete(item);
                    }
                });                
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}
