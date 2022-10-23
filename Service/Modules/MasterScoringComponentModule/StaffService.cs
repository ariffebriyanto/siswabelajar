using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.MasterScoringComponentSubdomain;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.MasterScoringComponentModule
{
    public interface IStaffService
    {
        List<Period> GetAllPeriod();
        List<Stage> GetStages();
        List<SubStage> GetAllSubStage();
        List<SubStage> GetSubStageByStageID(int IDStage);
        public int SearchScoringComponentType(string ScoringComponentType);
        bool InsertScoringComponentType(ScoringComponentType questionType);
        List<ScoringComponentType> GetAllScoringComponentType();
        List<ScoringComponent> GetAllScoringComponent();
        ScoringComponent GetScoringComponentByID(Guid IDScoringComponent);
        bool InsertScoringComponent(ScoringComponent question);
        bool UpdateScoringComponent(ScoringComponent question);
        bool DeleteScoringComponent(Guid IDScoringComponent);
        List<Position> GetPositions();
    }
    public class StaffService : IStaffService
    {
        private readonly IScoringComponentRepository scoringComponentRepository;
        private readonly IScoringComponentTypeRepository scoringComponentTypeRepository;
        private readonly IPeriodRepository periodRepository;
        private readonly IStageRepository stageRepository;
        private readonly ISubStageRepository subStageRepository;
        private readonly IPositionRepository positionRepository;
        private readonly UnitOfWork unitOfWork;

        public StaffService(IScoringComponentRepository scoringComponentRepository, 
            IScoringComponentTypeRepository scoringComponentTypeRepository,
            IPeriodRepository periodRepository,
            IStageRepository stageRepository,
            ISubStageRepository subStageRepository,
            IPositionRepository positionRepository,
            UnitOfWork unitOfWork)
        {
            this.scoringComponentRepository = scoringComponentRepository;
            this.scoringComponentTypeRepository = scoringComponentTypeRepository;
            this.periodRepository = periodRepository;
            this.stageRepository = stageRepository;
            this.subStageRepository = subStageRepository;
            this.positionRepository = positionRepository;
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

        public int SearchScoringComponentType(string ScoringComponentType)
        {
            try
            {
                ScoringComponentTypeDTO response = scoringComponentTypeRepository.SearchScoringComponentType(ScoringComponentType);
                if (response != null && !response.IDScoringComponentType.Equals(Guid.Empty))
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

        public bool InsertScoringComponentType(ScoringComponentType questionType)
        {
            bool result = true;
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(scoringComponentTypeRepository).ToUse(ctx);
                    ScoringComponentTypeDTO request = new ScoringComponentTypeDTO()
                    {
                        ScoringComponentType = questionType.ScoringComponentTypeText
                    };

                    scoringComponentTypeRepository.Insert(request);
                });
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public List<ScoringComponentType> GetAllScoringComponentType()
        {
            List<ScoringComponentTypeDTO> dto = scoringComponentTypeRepository.GetAllScoringComponentType().ToList();
            List<ScoringComponentType> result = new List<ScoringComponentType>();

            foreach(var item in dto)
            {
                result.Add(new ScoringComponentType()
                {
                    IDScoringComponentType = item.IDScoringComponentType,
                    ScoringComponentTypeText = item.ScoringComponentType
                });
            }
            return result;
        }

        public List<ScoringComponent> GetAllScoringComponent()
        {
            List<ScoringComponent> result = new List<ScoringComponent>();
            List<ScoringComponentDTO> dto = scoringComponentRepository.GetAllScoringComponent().ToList();
            List<PeriodDTO> periodList = periodRepository.GetAllPeriod().ToList();
            List<StageDTO> stageList = stageRepository.GetAllStage().ToList();
            List<SubStageDTO> subStageList = subStageRepository.GetAllSubStage().ToList();

            foreach(var item in dto)
            {
                PeriodDTO period = periodList.Where(x => x.IDPeriod == item.IDPeriod).FirstOrDefault();
                StageDTO stage = stageList.Where(x => x.IDStage == item.IDStage).FirstOrDefault();
                SubStageDTO subStage = subStageList.Where(x => x.IDSubStage == item.IDSubStage).FirstOrDefault();

                result.Add(new ScoringComponent()
                {
                    IDScoringComponent = item.IDScoringComponent,
                    IDScoringComponentType = item.IDScoringComponentType,
                    IDPeriod = item.IDPeriod,
                    IDStage = item.IDStage,
                    IDSubStage = item.IDSubStage,
                    IDPosition = item.IDPosition,
                    ScoringComponentText = item.ScoringComponent,
                    MinScore = item.MinScore,
                    MaxScore = item.MaxScore,
                    PeriodName = period == null ? "" : period.PeriodName,
                    StageName = stage == null ? "" : stage.StageName,
                    SubStageName = subStage == null ? "" : subStage.SubStageName
                });
            }
            return result;
        }

        public ScoringComponent GetScoringComponentByID(Guid IDScoringComponent)
        {
            ScoringComponentDTO dto = scoringComponentRepository.GetScoringComponentByID(IDScoringComponent);
            return new ScoringComponent()
            {
                IDScoringComponent = dto.IDScoringComponent,
                IDScoringComponentType = dto.IDScoringComponentType,
                IDPeriod = dto.IDPeriod,
                IDStage = dto.IDStage,
                IDSubStage = dto.IDSubStage,
                IDPosition = dto.IDPosition,
                ScoringComponentText = dto.ScoringComponent,
                MinScore = dto.MinScore,
                MaxScore = dto.MaxScore
            };
        }

        public bool InsertScoringComponent(ScoringComponent question)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(scoringComponentRepository).ToUse(ctx);

                    ScoringComponentDTO request = new ScoringComponentDTO()
                    {
                        IDScoringComponentType = question.IDScoringComponentType ?? Guid.Empty,
                        IDPeriod = question.IDPeriod,
                        IDStage = question.IDStage,
                        IDSubStage = question.IDSubStage,
                        IDPosition = question.IDPosition,
                        ScoringComponent = question.ScoringComponentText,
                        MinScore = question.MinScore,
                        MaxScore = question.MaxScore
                    };

                    scoringComponentRepository.Insert(request);
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateScoringComponent(ScoringComponent question)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(scoringComponentRepository).ToUse(ctx);

                    ScoringComponentDTO dto = scoringComponentRepository.GetScoringComponentByID(question.IDScoringComponent);
                    dto.IDScoringComponent = question.IDScoringComponent;
                    dto.IDScoringComponentType = question.IDScoringComponentType ?? Guid.Empty;
                    dto.IDPeriod = question.IDPeriod;
                    dto.IDStage = question.IDStage;
                    dto.IDSubStage = question.IDSubStage;
                    dto.IDPosition = question.IDPosition;
                    dto.ScoringComponent = question.ScoringComponentText;
                    dto.MinScore = question.MinScore;
                    dto.MaxScore = question.MaxScore;

                    scoringComponentRepository.Update(dto);
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteScoringComponent(Guid IDScoringComponent)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(scoringComponentRepository).ToUse(ctx);
                    ScoringComponentDTO dto = scoringComponentRepository.GetScoringComponentByID(IDScoringComponent);
                    scoringComponentRepository.Delete(dto);
                });

                return true;
            }
            catch
            {
                return false;
            }
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
    }
}
