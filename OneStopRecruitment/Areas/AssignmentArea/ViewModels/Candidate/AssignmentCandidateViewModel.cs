using Model.Subdomains.AssignmentSubdomain.Candidate;
using System;

namespace OneStopRecruitment.Areas.AssignmentArea.ViewModels.Candidate
{
    public class AssignmentCandidateViewModel
    {
        public MasterSchedule Schedule { get; set; }
        public Assignment Assignment { get; set; }
        public Submission Submission { get; set; }
        public Submission LastSubmission { get; set; }
        public int IDAssignment { get; set; }
        public Guid IDSubmission { get; set; }
    }
}
