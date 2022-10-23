using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Model.Subdomains.DashboardSubdomain.Candidate
{
    public class ToDo
    {
        public string ScheduleorAssignment { get; set; }
        public string PeriodName { get; set; }
        public string StageName { get; set; }
        public string SubStageName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime DeadlineStartDate { get; set; }
        public DateTime DeadlineEndDate { get; set; }
    }
}
