using Helper.StringHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Subdomains.LayoutSubdomain;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.ViewComponentHelpers;
using OneStopRecruitment.ViewModels;
using Service.Modules.LayoutModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneStopRecruitment.ViewComponents
{
    public class SideBar : ViewComponent
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMainService mainService;
        public SideBar(
            IHttpContextAccessor httpContextAccessor,
            IMainService mainService
        )
        {
            this.httpContextAccessor = httpContextAccessor;
            this.mainService = mainService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            SideBarViewModel sideBarViewModel = new SideBarViewModel();

            var Session = httpContextAccessor.HttpContext.Session.GetLoggedinUser();
            Role role = new Role();
            role.IDRole = Session.IDRole;
            role.RoleName = StringManipulation.RemoveSpace(Session.RoleName);

            List<SideBarItem> sideBars = mainService.GetModulesByRole(role.IDRole);
            sideBarViewModel.SideBarList = sideBars;            
            sideBarViewModel.UserRole = role;

            return View(ViewComponentPath.ViewPath("Layout", "_SideBar"), sideBarViewModel);
        }
    }
}
