using Helper.DropdownHelper;
using Microsoft.AspNetCore.Mvc;
using OneStopRecruitment.Areas.AssignmentArea.ViewModels.Staff;
using OneStopRecruitment.Helpers.ViewComponentHelpers;
using Service.Modules.AssignmentModule;
using System.Linq;
using System.Threading.Tasks;

namespace OneStopRecruitment.Areas.AssignmentArea.ViewComponents.Staff
{
    [ViewComponent(Name = "SearchAssignment")]
    public class SearchAssignment : ViewComponent
    {
        private readonly IStaffService staffService;
        public SearchAssignment(IStaffService staffService)
        {
            this.staffService = staffService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int IDPeriod, int IDStage, int IDSubStage)
        {
            AssignmentResultViewModel assignmentResultViewModel = new AssignmentResultViewModel();
            assignmentResultViewModel.AssignmentScheduleList = staffService.GetAssignmentsByPeriodStageSubStageId(IDPeriod, IDStage, DropdownManipulation.NormalizeEmptyDropdown(IDSubStage));
            if (assignmentResultViewModel.AssignmentScheduleList.Count > 0)
            {
                assignmentResultViewModel.IDAssignment = assignmentResultViewModel.AssignmentScheduleList.Select(x => x.IDAssignment).FirstOrDefault();
            }
            assignmentResultViewModel.IDPeriod = IDPeriod;
            assignmentResultViewModel.IDStage = IDStage;
            assignmentResultViewModel.IDSubStage = IDSubStage;
            return View(ViewComponentPath.AreaViewPath("AssignmentArea", "Staff", "_SearchAssignment"), assignmentResultViewModel);
        }
    }
}
