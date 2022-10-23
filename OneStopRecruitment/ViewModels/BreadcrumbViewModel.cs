using Model.Subdomains.LayoutSubdomain;
using System.Collections.Generic;

namespace OneStopRecruitment.ViewModels
{
    public class BreadcrumbViewModel
    {
        public List<BreadcrumbItem> Breadcrumb { get; set; }
        public Role UserRole { get; set; }
    }
}
