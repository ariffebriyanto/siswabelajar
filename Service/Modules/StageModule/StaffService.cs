using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.StageSubdomain;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.StageModule
{
    public interface IStaffService
    {
        List<Stage> GetStages();
        Stage GetStageById(int IDStage);
        bool InsertStage(Stage stage);
        bool UpdateStage(Stage stage);
    }
    public class StaffService : IStaffService
    {
        private readonly IStageRepository stageRepository;
        private readonly UnitOfWork unitOfWork;

        public StaffService(
            IStageRepository stageRepository,
            UnitOfWork unitOfWork
        )
        {
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

        public Stage GetStageById(int IDStage)
        {
            var stageById = stageRepository.GetStageById(IDStage);
            Stage stage = new Stage();
            stage.IDStage = IDStage;
            stage.StageName = stageById.StageName;
            stage.StageLevel = stageById.StageLevel;
            return stage;
        }

        public bool InsertStage(Stage stage)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(stageRepository).ToUse(ctx);
                    StageDTO stageDTO = new StageDTO
                    {
                        StageName = stage.StageName.Trim(),
                        StageLevel = stage.StageLevel
                    };
                    stageRepository.Insert(stageDTO);
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateStage(Stage stage)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(stageRepository).ToUse(ctx);
                    var stageById = stageRepository.GetStageById(stage.IDStage);
                    stageById.StageName = stage.StageName.Trim();
                    stageById.StageLevel = stage.StageLevel;
                    stageRepository.Update(stageById);
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
