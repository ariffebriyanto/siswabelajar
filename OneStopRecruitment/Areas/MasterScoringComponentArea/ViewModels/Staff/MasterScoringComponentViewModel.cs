using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Subdomains.MasterScoringComponentSubdomain;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.MasterScoringComponentArea.ViewModels.Staff
{
    public class MasterScoringComponentViewModel
    {
        public List<ScoringComponent> ScoringComponentList { get; set; }
        public ScoringComponentType ScoringComponentType { get; set; }
        public ScoringComponent ScoringComponent { get; set; }
        public List<SelectListItem> PeriodList { get; set; }
        public List<SelectListItem> StageList { get; set; }
        public List<SelectListItem> SubStageList { get; set; }
        public List<SelectListItem> ScoringComponentTypeList { get; set; }
        public List<Position> PositionList { get; set; }
    }
}
