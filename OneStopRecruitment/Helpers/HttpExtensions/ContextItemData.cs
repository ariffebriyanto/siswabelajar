using Microsoft.AspNetCore.Mvc;

namespace OneStopRecruitment.Helpers.HttpExtensions
{
    public static class ContextItemData
    {
        private static string ConvertFullAreaToAreaURLPrefix(string AreaName)
        {

            return AreaName.Replace("Area", "");
        }
        public static string GetURLWithAreaAndControllerAndAction(this ActionContext context)
        {
            string Area = context.RouteData.Values["Area"]?.ToString();

            if (Area == null)
            {
                return null;
            }

            string Controller = context.RouteData.Values["Controller"].ToString();
            string Action = context.RouteData.Values["Action"].ToString();

            string RequestURL = $"{ConvertFullAreaToAreaURLPrefix(Area)}/{Controller.Replace("Controller", "")}/{Action}";
            return RequestURL;
        }
    }
}