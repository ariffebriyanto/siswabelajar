using Model.Subdomains.AssignmentSubdomain.Trainer;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.AssignmentArea.ViewModels.Trainer
{
    public class AssignmentTrainerViewModel
    {
        public MasterSchedule Schedule { get; set; }
        public Assignment Assignment { get; set; }
        public List<CandidateAssignment> CandidateList { get; set; }
    }
}
