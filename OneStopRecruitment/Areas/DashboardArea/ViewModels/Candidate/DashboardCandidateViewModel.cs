using Model.Subdomains.DashboardSubdomain.Candidate;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.DashboardArea.ViewModels.Candidate
{
    public class DashboardCandidateViewModel
    {        
        public Dashboard Dashboard { get; set; }
        public List<MasterSchedule> ScheduleList { get; set; }
        public List<MasterSchedule> CandidateScheduleList { get; set; }
        public List<Assignment> AssignmentList { get; set; }
        public List<BlastEmail> BlastEmailList { get; set; }        
        public MasterSchedule LogicTestSchedule { get; set; }
        public List<ToDo> ToDoList { get; set; }
    }
}
