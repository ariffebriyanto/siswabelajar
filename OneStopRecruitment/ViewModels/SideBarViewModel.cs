using Model.Subdomains.LayoutSubdomain;
using System.Collections.Generic;

namespace OneStopRecruitment.ViewModels
{
    public class SideBarViewModel
    {
        public List<SideBarItem> SideBarList { get; set; }
        public Role UserRole { get; set; }
    }
}
