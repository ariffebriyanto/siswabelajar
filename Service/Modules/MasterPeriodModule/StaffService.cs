using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.MasterPeriodSubdomain;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.MasterPeriodModule
{
    public interface IStaffService
    {
        List<Period> GetAllPeriod();
        bool UpdateActivePeriod(int selectedPeriod);
        Period GetPeriodByID(int IDPeriod);
        bool InsertPeriod(Period period);
        bool UpdatePeriod(Period period);
    }
    public class StaffService : IStaffService
    {
        private readonly IPeriodRepository periodRepository;
        private readonly IStageRepository stageRepository;
        private readonly UnitOfWork unitOfWork;

        public StaffService(IPeriodRepository periodRepository, IStageRepository stageRepository, UnitOfWork unitOfWork)
        {
            this.periodRepository = periodRepository;
            this.stageRepository = stageRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<Period> GetAllPeriod()
        {
            List<PeriodDTO> periodList = periodRepository.FindAll().ToList();
            List<StageDTO> stageList = stageRepository.GetStagesByIDStageList(periodList.Select(x => x.IDStage).ToList()).ToList();

            List<Period> result = new List<Period>();
            foreach(PeriodDTO item in periodList)
            {
                Period period = new Period();
                period.IDPeriod = item.IDPeriod;
                period.IDStage = item.IDStage;
                period.StageName = stageList.Where(x => x.IDStage == item.IDStage).Select(x => x.StageName).FirstOrDefault();
                period.PeriodName = item.PeriodName;                
                period.IsActive = item.IsActive;
                period.DeadlineStart = item.DeadlineStart;
                period.DeadlineEnd = item.DeadlineEnd;
                period.IsComplete = item.IsComplete;

                result.Add(period);                
            }

            return result;
        }

        public bool UpdateActivePeriod(int selectedPeriod)
        {
            bool result = true;
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(periodRepository).ToUse(ctx);

                    var periodList = periodRepository.FindAll().ToList();
                    foreach (var period in periodList)
                    {
                        period.IsActive = period.IDPeriod == selectedPeriod ? 1 : 0;
                        periodRepository.Update(period);
                    }
                });                
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public Period GetPeriodByID(int IDPeriod)
        {
            try
            {
                PeriodDTO result = periodRepository.GetPeriodByID(IDPeriod);
                return new Period()
                {
                    IDPeriod = IDPeriod,
                    PeriodName = result.PeriodName,
                    DeadlineStart = result.DeadlineStart,
                    DeadlineEnd = result.DeadlineEnd,
                    IsActive = result.IsActive,
                    IsComplete = result.IsComplete
                };
            }
            catch
            {
                return null;
            }
        }

        public bool InsertPeriod(Period period)
        {
            bool result = true;
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(periodRepository).ToUse(ctx);

                    PeriodDTO request = new PeriodDTO()
                    {
                        IDStage = 1,
                        PeriodName = period.PeriodName,
                        DeadlineStart = period.DeadlineStart,
                        DeadlineEnd = period.DeadlineEnd,
                        IsActive = 0,
                        IsComplete = 0
                    };
                    periodRepository.Insert(request);
                });                
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool UpdatePeriod(Period period)
        {
            bool result = true;
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(periodRepository).ToUse(ctx);

                    PeriodDTO periodDTO = periodRepository.GetPeriodByID(period.IDPeriod);
                    periodDTO.PeriodName = period.PeriodName;
                    periodDTO.DeadlineStart = period.DeadlineStart;
                    periodDTO.DeadlineEnd = period.DeadlineEnd;
                    periodDTO.IsComplete = period.IsComplete;

                    periodRepository.Update(periodDTO);
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
