using Microsoft.AspNetCore.Mvc;
using OneStopRecruitment.Areas.UserArea.ViewModels;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.ViewComponentHelpers;
using Service.Modules.UserModule;
using System.Threading.Tasks;

namespace OneStopRecruitment.Areas.UserArea.ViewComponents.Staff
{
    [ViewComponent(Name = "SearchUser")]
    public class SearchUser : ViewComponent        
    {
        private readonly IStaffService userService;
        public SearchUser(IStaffService userService)
        {
            this.userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int IDRole)
        {
            UserViewModel userViewModel = new UserViewModel()
            {
                IDRole = IDRole,
                UserList = userService.GetUsersByRoleID(IDRole),
                SessionUsername = HttpContext.Session.GetLoggedinUser().Username
            };            
            return View(ViewComponentPath.AreaViewPath("UserArea", "Staff", "_SearchUser"), userViewModel);
        }
    }
}