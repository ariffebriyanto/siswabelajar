using Helper.StringHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model.Subdomains.LayoutSubdomain;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.ViewComponentHelpers;
using OneStopRecruitment.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneStopRecruitment.ViewComponents
{
    public class Breadcrumb : ViewComponent
    {
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;
        public Breadcrumb(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            BreadcrumbViewModel breadcrumbViewModel = new BreadcrumbViewModel();

            List<BreadcrumbItem> breadcrumbItemList = new List<BreadcrumbItem>();
            string CurrentURL = ViewContext.GetURLWithAreaAndControllerAndAction();
            if (CurrentURL != null)
            {
                string appendURL = configuration.GetSection("BaseURL").Value;
                List<string> splitted = StringManipulation.SplitToList(CurrentURL, "/");
                foreach(var item in splitted)
                {
                    appendURL += "/" + item;

                    BreadcrumbItem breadcrumbItem = new BreadcrumbItem();
                    breadcrumbItem.URL = appendURL;
                    breadcrumbItem.Name = StringManipulation.AddSpace(item);
                    breadcrumbItemList.Add(breadcrumbItem);
                }
            }
            breadcrumbViewModel.Breadcrumb = breadcrumbItemList;

            var Session = httpContextAccessor.HttpContext.Session.GetLoggedinUser();
            Role role = new Role();
            role.IDRole = Session.IDRole;
            role.RoleName = StringManipulation.RemoveSpace(Session.RoleName);
            breadcrumbViewModel.UserRole = role;

            return View(ViewComponentPath.ViewPath("Layout", "_Breadcrumb"), breadcrumbViewModel);
        }
    }
}
