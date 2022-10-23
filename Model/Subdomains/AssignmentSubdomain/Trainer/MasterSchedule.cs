using System;

namespace Model.Subdomains.AssignmentSubdomain.Trainer
{
    public class MasterSchedule
    {                
        public Guid IDSchedule { get; set; }        
        public int IDPeriod { get; set; }
        public int IDStage { get; set; }        
        public int IDSubStage { get; set; }        
        public string IDPosition { get;set; }      
        public int IDAssignment { get; set; }
        public DateTime Date { get; set; }        
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }        
        public string Room { get; set; }        
        public int Limit { get; set; }
        public int Qty { get; set; }
        public int CountSubmission { get; set; }
        public Guid IDReviewer { get; set; }
        public string PeriodName { get; set; }
        public string StageName { get; set; }
        public string SubStageName { get; set; }
        public string ReviewerName { get; set; }
        public DateTime DeadlineStart { get; set; }
        public DateTime DeadlineEnd { get; set; }
        public bool Status { get; set; }
    }
}
