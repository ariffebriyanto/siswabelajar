using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.MinimumScoreSubdomain;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.MinimumScoreModule
{
    public interface IStaffService
    {
        List<Period> GetAllPeriod();
        List<Stage> GetStages();
        List<SubStage> GetAllSubStage();
        List<SubStage> GetSubStageByStageID(int IDStage);
        List<MinimumScore> GetAllMinimumScore();
        MinimumScore GetMinimumScoreByID(int IDMinimumScore);
        bool CheckMinimumScore(int IDMinimumScore, int IDPeriod, int IDStage, int IDSubStage);
        bool InsertMinimumScore(MinimumScore minimumScore);
        bool UpdateMinimumScore(MinimumScore minimumScore);
        bool DeleteMinimumScore(int IDMinimumScore);
    }
    public class StaffService : IStaffService
    {
        private readonly IMinimumScoreRepository minimumScoreRepository;
        private readonly IPeriodRepository periodRepository;
        private readonly IStageRepository stageRepository;
        private readonly ISubStageRepository subStageRepository;
        private readonly UnitOfWork unitOfWork;

        public StaffService(IMinimumScoreRepository minimumScoreRepository,
            IPeriodRepository periodRepository, IStageRepository stageRepository, 
            ISubStageRepository subStageRepository, UnitOfWork unitOfWork)
        {
            this.minimumScoreRepository = minimumScoreRepository;
            this.periodRepository = periodRepository;
            this.stageRepository = stageRepository;
            this.subStageRepository = subStageRepository;
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

        public List<SubStage> GetAllSubStage()
        {
            List<SubStageDTO> subStageDTO = subStageRepository.FindAll().ToList();
            List<SubStage> result = new List<SubStage>();

            foreach (var item in subStageDTO)
            {
                StageDTO stageDTO = stageRepository.GetStageById(item.IDStage);
                result.Add(new SubStage()
                {
                    IDSubStage = item.IDSubStage,
                    IDStage = item.IDStage,
                    SubStageName = item.SubStageName,
                    StageName = stageDTO.StageName
                });
            }
            return result;
        }

        public List<SubStage> GetSubStageByStageID(int IDStage)
        {
            List<SubStageDTO> subStageDTO = subStageRepository.GetSubStageByStageID(IDStage).ToList();
            List<SubStage> result = new List<SubStage>();

            foreach (var item in subStageDTO)
            {
                StageDTO stageDTO = stageRepository.GetStageById(item.IDStage);
                result.Add(new SubStage()
                {
                    IDSubStage = item.IDSubStage,
                    IDStage = item.IDStage,
                    SubStageName = item.SubStageName,
                    StageName = stageDTO.StageName
                });
            }
            return result;
        }

        public List<MinimumScore> GetAllMinimumScore()
        {
            List<MinimumScoreDTO> dto = minimumScoreRepository.FindAll().ToList();
            List<MinimumScore> result = new List<MinimumScore>();

            foreach (var item in dto)
            {
                PeriodDTO period = periodRepository.GetPeriodByID(item.IDPeriod);
                StageDTO stage = stageRepository.GetStageById(item.IDStage);
                SubStageDTO subStage = subStageRepository.GetSubStageByID(item.IDSubStage);

                result.Add(new MinimumScore()
                {
                    IDMinimumScore = item.IDMinimumScore,
                    IDPeriod = item.IDPeriod,
                    IDStage = item.IDStage,
                    IDSubStage = subStage == null ? 0 : item.IDSubStage,
                    Score = item.MinimumScore,
                    StageName = stage.StageName,
                    SubStageName = subStage == null ? "" : subStage.SubStageName,
                    PeriodName = period.PeriodName
                }); ;
            }

            return result;
        }
        public MinimumScore GetMinimumScoreByID(int IDMinimumScore)
        {
            MinimumScoreDTO minimumScore = minimumScoreRepository.GetMinimumScoreByID(IDMinimumScore);
            PeriodDTO period = periodRepository.GetPeriodByID(minimumScore.IDPeriod);
            StageDTO stage = stageRepository.GetStageById(minimumScore.IDStage);
            SubStageDTO subStage = subStageRepository.GetSubStageByID(minimumScore.IDSubStage);

            return new MinimumScore()
            {
                IDMinimumScore = minimumScore.IDMinimumScore,
                IDPeriod = minimumScore.IDPeriod,
                IDStage = minimumScore.IDStage,
                IDSubStage = subStage == null ? 0 : minimumScore.IDSubStage,
                Score = minimumScore.MinimumScore,
                StageName = stage.StageName,
                SubStageName = subStage == null ? "" : subStage.SubStageName,
                PeriodName = period.PeriodName
            };
        }

        public bool CheckMinimumScore(int IDMinimumScore, int IDPeriod, int IDStage, int IDSubStage)
        {            
            try
            {
                MinimumScoreDTO dto = minimumScoreRepository.CheckMinimumScore(IDPeriod, IDStage, IDSubStage);
                if (IDMinimumScore == 0)
                {
                    if (dto != null && dto.IDMinimumScore != 0)
                    {
                        return true;
                    }
                }
                else
                {
                    // If the MinimumScore was found the same with the one want to be updated
                    if (dto != null && dto.IDMinimumScore != 0 && IDMinimumScore != dto.IDMinimumScore)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }            
        }

        public bool InsertMinimumScore(MinimumScore minimumScore)
        {
            bool result = true;
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(minimumScoreRepository).ToUse(ctx);

                    MinimumScoreDTO request = new MinimumScoreDTO()
                    {
                        IDPeriod = minimumScore.IDPeriod,
                        IDStage = minimumScore.IDStage,
                        IDSubStage = minimumScore.IDSubStage,
                        MinimumScore = minimumScore.Score
                    };
                    minimumScoreRepository.Insert(request);
                });
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public bool UpdateMinimumScore(MinimumScore minimumScore)
        {
            bool result = true;
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(minimumScoreRepository).ToUse(ctx);

                    MinimumScoreDTO dto = minimumScoreRepository.GetMinimumScoreByID(minimumScore.IDMinimumScore);
                    dto.IDPeriod = minimumScore.IDPeriod;
                    dto.IDStage = minimumScore.IDStage;
                    dto.IDSubStage = minimumScore.IDSubStage;
                    dto.MinimumScore = minimumScore.Score;

                    minimumScoreRepository.Update(dto);
                });
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool DeleteMinimumScore(int IDMinimumScore)
        {
            bool result = true;
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(minimumScoreRepository).ToUse(ctx);

                    MinimumScoreDTO dto = minimumScoreRepository.GetMinimumScoreByID(IDMinimumScore);                    
                    minimumScoreRepository.Delete(dto);
                });
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
