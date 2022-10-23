using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Subdomains.MinimumScoreSubdomain;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.MinimumScoreArea.ViewModels
{
    public class MinimumScoreViewModel
    {
        public MinimumScore MinimumScore;

        public List<MinimumScore> MinimumScoreList;

        public List<SelectListItem> StageList;

        public List<SelectListItem> SubStageList;

        public List<SelectListItem> PeriodList;
    }
}
