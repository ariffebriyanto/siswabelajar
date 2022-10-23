using Microsoft.AspNetCore.Http;
using System;

namespace Model.Subdomains.AssignmentSubdomain.Candidate
{
    public class Assignment
    {
        public int IDAssignment { get; set; }        
        public int IDPeriod { get; set; }
        public string PeriodName { get; set; }
        public int IDStage { get; set; }
        public string StageName { get; set; }
        public int IDSubStage { get; set; }
        public string SubStageName { get; set; }        
        public DateTime? DeadlineStartDate { get; set; }
        public DateTime? DeadlineEndDate { get; set; }
        public string Room { get; set; }        
        public string Notes { get; set; }        
        public IFormFile AssignmentFile { get; set; }
        public string AssignmentFileName { get; set; }
        public Guid IDTrainer { get; set; }
        public string TrainerName { get; set; }
        public DateTime ScheduleDate { get; set; }
        public TimeSpan ScheduleStartTime { get; set; }
        public TimeSpan ScheduleEndTime { get; set; }
    }
}
