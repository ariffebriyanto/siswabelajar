using Microsoft.AspNetCore.Mvc;
using OneStopRecruitment.Areas.MasterScheduleArea.ViewModels.Staff;
using OneStopRecruitment.Helpers.ViewComponentHelpers;
using Service.Modules.MasterScheduleModule;
using System.Threading.Tasks;

namespace OneStopRecruitment.Areas.MasterScheduleArea.ViewComponents.Staff
{
    [ViewComponent(Name = "TableSchedule")]
    public class TableSchedule : ViewComponent
    {
        private readonly IStaffService staffService;
        public TableSchedule(IStaffService staffService)
        {
            this.staffService = staffService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int IDPeriod, int IDStage, string IDPosition)
        {
            MasterScheduleViewModel viewModel = new MasterScheduleViewModel();
            viewModel.ScheduleList = staffService.GetScheduleByPeriodAndStage(IDPeriod, IDStage, IDPosition);
            return View(ViewComponentPath.AreaViewPath("MasterScheduleArea", "Staff", "_TableSchedule"), viewModel);
        }
    }
}
