using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.SubStageSubdomain;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.SubStageModule
{
    public interface IStaffService
    {
        List<Stage> GetStages();
        List<SubStage> GetAllSubStage();
        List<SubStage> GetSubStageByStageID(int IDStage);
        SubStage GetSubStageByID(int IDSubStage);
        bool InsertSubStage(SubStage subStage);
        bool UpdateSubStage(SubStage subStage);
    }
    public class StaffService : IStaffService
    {
        private readonly ISubStageRepository subStageRepository;
        private readonly IStageRepository stageRepository;
        private readonly UnitOfWork unitOfWork;
        public StaffService(ISubStageRepository subStageRepository, IStageRepository stageRepository, UnitOfWork unitOfWork)
        {
            this.subStageRepository = subStageRepository;
            this.stageRepository = stageRepository;
            this.unitOfWork = unitOfWork;
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

            foreach(var item in subStageDTO)
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

        public SubStage GetSubStageByID(int IDSubStage)
        {            
            SubStageDTO subStageDTOs = subStageRepository.GetSubStageByID(IDSubStage);
            StageDTO stageDTOs = stageRepository.GetStageById(subStageDTOs.IDStage);
            return new SubStage()
            {
                IDSubStage = IDSubStage,
                IDStage = subStageDTOs.IDStage,
                SubStageName = subStageDTOs.SubStageName,
                StageName = stageDTOs.StageName
            };
        }

        public bool InsertSubStage(SubStage subStage)
        {
            bool result = true;
            try
            {
                unitOfWork.Run((r, ctx) =>
                {                                     
                    r.ConvertContextOfRepository(subStageRepository).ToUse(ctx);

                    SubStageDTO request = new SubStageDTO()
                    {
                        IDSubStage = subStage.IDSubStage,
                        IDStage = subStage.IDStage,
                        SubStageName = subStage.SubStageName
                    };

                    subStageRepository.Insert(request);
                });                
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool UpdateSubStage(SubStage subStage)
        {
            bool result = true;
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(subStageRepository).ToUse(ctx);

                    SubStageDTO dto = subStageRepository.GetSubStageByID(subStage.IDSubStage);
                    dto.IDStage = subStage.IDStage;
                    dto.SubStageName = subStage.SubStageName;
                    subStageRepository.Update(dto);
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
