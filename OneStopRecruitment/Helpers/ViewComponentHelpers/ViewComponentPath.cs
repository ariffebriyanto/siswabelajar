namespace OneStopRecruitment.Helpers.ViewComponentHelpers
{
    public class ViewComponentPath
    {
        public static string ViewPath(string ModuleName, string ViewName) => $"~/Views/{ModuleName}/Components/{ViewName}.cshtml";
        public static string AreaViewPath(string Area, string ModuleName, string ViewName) => $"~/Areas/{Area}/Views/{ModuleName}/Components/{ViewName}.cshtml";
    }
}
