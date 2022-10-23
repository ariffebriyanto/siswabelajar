using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Subdomains.SubStageSubdomain;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.SubstageArea.ViewModels
{
    public class SubstageViewModel
    {
        public SubStage SubStage { get; set; }
        public List<SubStage> SubStageList { get; set; }
        public List<SelectListItem> StageList { get; set; }
    }
}
