using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Model.Subdomains.MiddlewareSubdomain;
using OneStopRecruitment.Helpers.ActionFilterExtensions;
using OneStopRecruitment.Helpers.HttpExtensions;
using Service.Modules.MiddlewareModule;
using System.Collections.Generic;

namespace OneStopRecruitment.Middlewares.ActionFilters
{
    public class SetCurrentMenuOrRedirectActionFilter : IActionFilter
    {
        private readonly IModuleService moduleService;
        public SetCurrentMenuOrRedirectActionFilter(IModuleService moduleService)
        {
            this.moduleService = moduleService;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string RequestURL = context.GetURLWithAreaAndControllerAndAction();
            List<Module> modules = moduleService.GetModuleByUrl(RequestURL);
           
            if (modules != null && modules.Count != 0)
            {
                if (modules.Count > 1)
                {
                    List<int> RoleIds= new List<int>();
                    foreach (var item in modules)
                    {
                        RoleIds.Add(item.IDRole);
                    }

                    context.SetCurrentModuleData(new ModuleData
                    {
                        IDModule = modules[0].IDModule,
                        Name = modules[0].ModuleName,
                        Link = modules[0].Route,
                        RoleIds = RoleIds
                    });

                    return;
                }
                else if (modules.Count == 1)
                {
                    var module = modules[0];

                    context.SetCurrentModuleData(new ModuleData
                    {
                        IDModule = module.IDModule,
                        Name = module.ModuleName,
                        Link = module.Route,
                        IDRole = module.IDRole
                    });

                    return;
                }
            }

            context.Result = new RedirectToRouteResult(
                                new RouteValueDictionary
                                {
                                    { "action", "Index" },
                                    { "controller", "PageNotFound" }
                                }
                            );
        }
    }
}
