using System.Collections.Generic;

namespace Model.Subdomains.DashboardSubdomain.Staff
{
    public class DashboardStaff
    {
        public string CurrentPeriod { get; set; }
        public string CurrentStage { get; set; }
        public string NumberOfCandidates { get; set; }
        public List<MasterSchedule> UpcomingSchedules { get; set; }
    }
}
