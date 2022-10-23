using Model.Subdomains.DashboardSubdomain.Trainer;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.DashboardArea.ViewModels.Trainer
{
    public class DashboardTrainerViewModel
    {
        public Dashboard Dashboard { get; set; }
        public List<MasterSchedule> TrainerAssignmentSchedule { get; set; }
        public List<MasterSchedule> AvailableAssignmentSchedule { get; set; }
        public List<CandidateSchedule> CandidateList { get; set; }
    }
}
