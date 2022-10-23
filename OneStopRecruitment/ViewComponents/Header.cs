using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Subdomains.LoginSubdomain;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.ViewComponentHelpers;
using OneStopRecruitment.ViewModels;
using System;
using System.Threading.Tasks;

namespace OneStopRecruitment.ViewComponents
{
    public class Header : ViewComponent
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public Header(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public static string GetGreetings()
        {
            try
            {
                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour <= 4)
                {
                    return "Good Evening";
                }
                else if (DateTime.Now.Hour >= 5 && DateTime.Now.Hour <= 11)
                {
                    return "Good Morning";
                }
                else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour <= 16)
                {
                    return "Good Afternoon";
                }
                else if (DateTime.Now.Hour >= 17 && DateTime.Now.Hour <= 24)
                {
                    return "Good Evening";
                }
                else
                {
                    return "Hello";
                }
            }
            catch
            {
                return "Hello";
            }
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderViewModel headerViewModel = new HeaderViewModel();

            var Session = httpContextAccessor.HttpContext.Session.GetLoggedinUser();
            User user = new User();
            user.IDUser = Session.IDUser;
            user.Name = Session.Name;
            user.Username = Session.Username;
            user.Email = Session.Email;
            user.IDRole = Session.IDRole;
            user.RoleName = Session.RoleName;
            headerViewModel.User = user;

            headerViewModel.Greetings = GetGreetings();

            return View(ViewComponentPath.ViewPath("Layout", "_Header"), headerViewModel);
        }
    }
}
