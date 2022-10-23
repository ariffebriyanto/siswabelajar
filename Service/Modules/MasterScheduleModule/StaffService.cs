using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.MasterScheduleSubdomain;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.MasterScheduleModule
{
    public interface IStaffService
    {
        List<Period> GetAllPeriod();
        List<Stage> GetStages();
        List<SubStage> GetSubStageByStageID(int IDStage);
        List<Position> GetPositions();
        bool InsertSchedule(MasterSchedule schedule);
        bool UpdateSchedule(MasterSchedule schedule);
        List<MasterSchedule> GetScheduleByPeriodAndStage(int IDPeriod, int IDStage, string IDPosition);
        MasterSchedule GetScheduleByID(Guid IDSchedule);
    }
    public class StaffService : IStaffService
    {
        private readonly IMasterScheduleRepository scheduleRepository;
        private readonly IPeriodRepository periodRepository;
        private readonly IStageRepository stageRepository;
        private readonly ISubStageRepository subStageRepository;
        private readonly IPositionRepository positionRepository;
        private readonly ITransactionScheduleRepository transactionScheduleRepository;
        private readonly UnitOfWork unitOfWork;
        public StaffService(IMasterScheduleRepository scheduleRepository,
            IPeriodRepository periodRepository,
            IStageRepository stageRepository,
            ISubStageRepository subStageRepository,
            IPositionRepository positionRepository,
            ITransactionScheduleRepository transactionScheduleRepository,
            UnitOfWork unitOfWork)
        {
            this.scheduleRepository = scheduleRepository;
            this.periodRepository = periodRepository;
            this.stageRepository = stageRepository;
            this.subStageRepository = subStageRepository;
            this.positionRepository = positionRepository;
            this.transactionScheduleRepository = transactionScheduleRepository;
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
            var allStages = stageRepository.FindAll();
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

        public List<Position> GetPositions()
        {
            List<Position> positions = new List<Position>();
            var allPosition = positionRepository.FindAll();
            foreach (var item in allPosition)
            {
                Position position = new Position();
                position.IDPosition = item.IDPosition;
                position.PositionName = item.PositionName;
                positions.Add(position);
            }
            return positions;
        }


        public bool InsertSchedule(MasterSchedule schedule)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(scheduleRepository).ToUse(ctx);
                    MasterScheduleDTO request = new MasterScheduleDTO()
                    {
                        IDPeriod = schedule.IDPeriod,
                        IDStage = schedule.IDStage,
                        IDSubStage = schedule.IDSubStage,
                        IDPosition = schedule.IDPosition,
                        Date = schedule.Date,
                        StartTime = schedule.StartTime,
                        EndTime = schedule.EndTime,
                        Room = schedule.Room,
                        Limit = schedule.Limit
                    };

                    scheduleRepository.Insert(request);
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateSchedule(MasterSchedule schedule)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(scheduleRepository).ToUse(ctx);

                    MasterScheduleDTO dto = scheduleRepository.GetScheduleByIDSchedule(schedule.IDSchedule);
                    List<TransactionScheduleDTO> transactionSchedule = transactionScheduleRepository.GetByIDSchedule(schedule.IDSchedule).ToList();
                    int qty = transactionSchedule == null ? 0 : transactionSchedule.Count();

                    if(schedule.Limit >= qty)
                    {
                        dto.IDPeriod = schedule.IDPeriod;
                        dto.IDStage = schedule.IDStage;
                        dto.IDSubStage = schedule.IDSubStage;
                        dto.IDPosition = schedule.IDPosition;
                        dto.Date = schedule.Date;
                        dto.StartTime = schedule.StartTime;
                        dto.EndTime = schedule.EndTime;
                        dto.Room = schedule.Room;
                        dto.Limit = schedule.Limit;

                        scheduleRepository.Update(dto);
                    }
                    else
                    {
                        throw new Exception("Invalid Limit");
                    }                                        
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<MasterSchedule> GetScheduleByPeriodAndStage(int IDPeriod, int IDStage, string IDPosition)
        {
            List<MasterScheduleDTO> dto = scheduleRepository.GetScheduleByPeriodStagePosition(IDPeriod, IDStage, IDPosition).ToList();
            List<PeriodDTO> periodList = periodRepository.GetAllPeriod().ToList();
            List<StageDTO> stageList = stageRepository.GetAllStage().ToList();
            List<SubStageDTO> subStageList = subStageRepository.GetAllSubStage().ToList();            
            List<MasterSchedule> result = new List<MasterSchedule>();

            List<Guid> IDScheduleList = dto == null ? new List<Guid>() : dto.Select(x => x.IDSchedule).ToList();
            List<TransactionScheduleDTO> transactionScheduleDTOs = transactionScheduleRepository.GetByIDScheduleList(IDScheduleList).ToList();            

            foreach (var item in dto)
            {
                PeriodDTO period = periodList.Where(x => x.IDPeriod == item.IDPeriod).FirstOrDefault();
                StageDTO stage = stageList.Where(x => x.IDStage == item.IDStage).FirstOrDefault();
                SubStageDTO subStage = subStageList.Where(x => x.IDSubStage == item.IDSubStage).FirstOrDefault();
                int qty = transactionScheduleDTOs != null ? transactionScheduleDTOs.Where(x => x.IDSchedule.Equals(item.IDSchedule)).Count() : 0;

                result.Add(new MasterSchedule()
                {
                    IDSchedule = item.IDSchedule,
                    IDPeriod = item.IDPeriod,
                    IDStage = item.IDStage,
                    IDSubStage = item.IDSubStage,
                    IDPosition = item.IDPosition,
                    Date = item.Date,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Room = item.Room,
                    Limit = item.Limit,                   
                    Qty = qty,
                    PeriodName =  period == null ? "" : period.PeriodName,
                    StageName = stage == null ? "" : stage.StageName,
                    SubStageName = subStage == null ? "" : subStage.SubStageName,
                });
            }
            return result;
        }

        public MasterSchedule GetScheduleByID(Guid IDSchedule)
        {
            MasterScheduleDTO dto = scheduleRepository.GetScheduleByIDSchedule(IDSchedule);
            return new MasterSchedule()
            {
                IDSchedule = dto.IDSchedule,
                IDPeriod = dto.IDPeriod,
                IDStage = dto.IDStage,
                IDSubStage = dto.IDSubStage,
                IDPosition = dto.IDPosition,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Room = dto.Room,
                Limit = dto.Limit
            };
        }
    }
}
