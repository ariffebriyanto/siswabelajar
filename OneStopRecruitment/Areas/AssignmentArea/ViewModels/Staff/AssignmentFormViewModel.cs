using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Subdomains.AssignmentSubdomain.Staff;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.AssignmentArea.ViewModels.Staff
{
    public class AssignmentFormViewModel
    {
        public List<SelectListItem> StageList { get; set; }
        public List<SelectListItem> SubStageList { get; set; }
        public Assignment AssignmentForm { get; set; }
    }
}
