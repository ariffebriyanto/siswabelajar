using Model.Subdomains.AssignmentSubdomain.Staff;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.AssignmentArea.ViewModels.Staff
{
    public class AssignmentDetailsViewModel
    {
        public AssignmentSchedule Schedule { get; set; }
        public List<Submission> SubmissionList { get; set; }
    }
}
