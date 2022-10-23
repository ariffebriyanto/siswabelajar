using Microsoft.AspNetCore.Mvc;
using OneStopRecruitment.Areas.MasterCandidateArea.ViewModels;
using OneStopRecruitment.Helpers.ViewComponentHelpers;
using Service.Modules.MasterCandidateModule;
using System.Threading.Tasks;

namespace OneStopRecruitment.Areas.MasterCandidateArea.ViewComponents.Staff
{
    [ViewComponent(Name = "TableCandidate")]
    public class TableCandidate : ViewComponent
    {
        private readonly IStaffService candidateService;

        public TableCandidate(IStaffService candidateService)
        {
            this.candidateService = candidateService;
        }

        public async Task<IViewComponentResult> InvokeAsync(CandidateViewModel viewModel)
        {                        
            viewModel.CanSubmitCandidate = candidateService.CanSubmitCandidate(viewModel.IDPeriod, viewModel.IDStage);
            viewModel.SubStageList = candidateService.GetSubStageBySchedule(viewModel.IDPeriod, viewModel.IDStage, viewModel.IDPosition);
            if (viewModel.SubStageList != null && viewModel.SubStageList.Count > 0)
            {
                return await CandidateWithSubStageTable(viewModel);
            }
            else
            {
                switch (viewModel.IDStage)
                {
                    case 1:
                        // Screening Test
                        return await ScreeningTestTable(viewModel);
                    case 2:
                        // Logic test
                        return await LogicTestTable(viewModel);
                    default:                        
                        return await CandidateWithoutSubStageTable(viewModel);
                }
            }            
        }

        public async Task<IViewComponentResult> ScreeningTestTable(CandidateViewModel viewModel)
        {
            if(viewModel.CandidateList == null || viewModel.CandidateList.Count == 0)
            {
                viewModel.CandidateList = candidateService.GetScreeningTestCandidate(viewModel.IDPeriod, viewModel.IDPosition);
            }            
            return View(ViewComponentPath.AreaViewPath("MasterCandidateArea", "Staff", "_TableScreeningTestCandidate"), viewModel);
        }

        public async Task<IViewComponentResult> LogicTestTable(CandidateViewModel viewModel)
        {
            if (viewModel.CandidateList == null || viewModel.CandidateList.Count == 0)
            {
                viewModel.CandidateList = candidateService.GetLogicTestCandidate(viewModel.IDPeriod, viewModel.IDPosition);
            }
            return View(ViewComponentPath.AreaViewPath("MasterCandidateArea", "Staff", "_TableLogicTestCandidate"), viewModel);
        }

        public async Task<IViewComponentResult> CandidateWithSubStageTable(CandidateViewModel viewModel)
        {
            if (viewModel.CandidateList == null || viewModel.CandidateList.Count == 0)
            {
                viewModel.CandidateList = candidateService.GetCandidateScoreWithSubStage(viewModel.IDPeriod, viewModel.IDStage, viewModel.IDPosition);
            }
            return View(ViewComponentPath.AreaViewPath("MasterCandidateArea", "Staff", "_TableSubStageCandidate"), viewModel);
        }

        public async Task<IViewComponentResult> CandidateWithoutSubStageTable(CandidateViewModel viewModel)
        {
            if (viewModel.CandidateList == null || viewModel.CandidateList.Count == 0)
            {
                viewModel.CandidateList = candidateService.GetCandidateScoreWithoutSubStage(viewModel.IDPeriod, viewModel.IDStage, viewModel.IDPosition);
            }
            return View(ViewComponentPath.AreaViewPath("MasterCandidateArea", "Staff", "_TableWithoutSubStageCandidate"), viewModel);
        }
    }
}
