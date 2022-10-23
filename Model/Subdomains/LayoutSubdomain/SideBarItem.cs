using System;

namespace Model.Subdomains.LayoutSubdomain
{
    public class SideBarItem
    {
        public Guid IDModule { get; set; }
        public string ModuleName { get; set; }
        public int ModuleLevel { get; set; }
        public string ModuleArea { get; set; }
        public string ModuleController { get; set; }
        public string ModuleAction { get; set; }
    }
}
