using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OneStopRecruitment.Helpers.ActionFilterExtensions;
using OneStopRecruitment.Helpers.HttpExtensions;
using System.Net;

namespace OneStopRecruitment.Middlewares.ActionFilters
{
    public class AuthenticationActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var loggedInUser = context.HttpContext.Session.GetLoggedinUser();

            if (loggedInUser == null)
            {
                if (context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
                {
                    context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
                    return;
                }

                context.Result = new RedirectResult("/Login");
                return;
            }

            ModuleData currentModule = context.GetCurrentModuleData();
            if (currentModule.RoleIds.Count > 0)
            {
                if (!currentModule.RoleIds.Contains(loggedInUser.IDRole))
                {
                    context.Result = new RedirectResult("/PageNotFound/Index");
                }
            }
            else if (loggedInUser.IDRole != currentModule.IDRole)
            {
                context.Result = new RedirectResult("/PageNotFound/Index");
            }
        }
    }
}