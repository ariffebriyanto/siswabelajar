using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Subdomains.MasterScheduleSubdomain;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.MasterScheduleArea.ViewModels.Staff
{
    public class MasterScheduleViewModel
    {
        public List<MasterSchedule> ScheduleList { get; set; }
        public MasterSchedule Schedule { get; set; }
        public List<SelectListItem> PeriodList { get; set; }
        public List<SelectListItem> StageList { get; set; }
        public List<Position> PositionList { get; set; }
        public List<SelectListItem> PositionDropdown { get; set; }
        public int IDPeriod { get; set; }
        public int IDStage { get; set; }
        public string IDPosition { get; set; }
    }
}
