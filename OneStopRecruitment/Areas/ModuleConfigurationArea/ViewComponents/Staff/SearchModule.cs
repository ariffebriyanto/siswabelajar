using Microsoft.AspNetCore.Mvc;
using OneStopRecruitment.Areas.ModuleConfigurationArea.ViewModels.Staff;
using OneStopRecruitment.Helpers.ViewComponentHelpers;
using Service.Modules.ModuleConfigurationModule;
using System.Threading.Tasks;

namespace OneStopRecruitment.Areas.ModuleConfigurationArea.ViewComponents.Staff
{
    [ViewComponent(Name = "SearchModule")]
    public class SearchModule : ViewComponent
    {
        private readonly IStaffService staffService;
        public SearchModule(IStaffService staffService)
        {
            this.staffService = staffService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int IDRole)
        {
            ModuleConfigurationResultViewModel moduleConfigurationResultViewModel = new ModuleConfigurationResultViewModel();
            moduleConfigurationResultViewModel.ModuleList = staffService.GetModuleByIdRole(IDRole);
            moduleConfigurationResultViewModel.IDRole = IDRole;
            moduleConfigurationResultViewModel.RoleName = staffService.GetRoleNameById(IDRole);
            return View(ViewComponentPath.AreaViewPath("ModuleConfigurationArea", "Staff", "_SearchModule"), moduleConfigurationResultViewModel);
        }
    }
}
