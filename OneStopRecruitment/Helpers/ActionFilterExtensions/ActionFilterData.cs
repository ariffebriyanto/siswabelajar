using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OneStopRecruitment.Helpers.ActionFilterExtensions
{
    public class ModuleData
    {
        public Guid IDModule { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int IDRole { get; set; }
        public List<int> RoleIds { get; set; }
        public ModuleData()
        {
            this.RoleIds = new List<int>();
        }
    }

    public static class ActionFilterData
    {
        private const string CURRENT_MENU_DATA = "CURRENT_MENU_DATA";

        public static ModuleData GetCurrentModuleData(this FilterContext filterContext) => JsonConvert.DeserializeObject<ModuleData>(filterContext.RouteData.Values[CURRENT_MENU_DATA].ToString());

        public static void SetCurrentModuleData(this FilterContext filterContext, ModuleData moduleData) => filterContext.RouteData.Values[CURRENT_MENU_DATA] = JsonConvert.SerializeObject(moduleData);
    }
}