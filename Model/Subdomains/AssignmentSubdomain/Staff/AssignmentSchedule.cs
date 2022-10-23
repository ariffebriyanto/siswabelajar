using System;

namespace Model.Subdomains.AssignmentSubdomain.Staff
{
    public class AssignmentSchedule
    {
        public Guid IDSchedule { get; set; }
        public int IDAssignment { get; set; }
        public int IDPeriod { get; set; }
        public string PeriodName { get; set; }
        public int IDStage { get; set; }
        public string StageName { get; set; }
        public int IDSubStage { get; set; }
        public string SubStageName { get; set; }
        public DateTime ScheduleDate { get; set; }
        public TimeSpan ScheduleStartTime { get; set; }
        public TimeSpan ScheduleEndTime { get; set; }
        public string Room { get; set; }
        public int Limit { get; set; }
        public int Quantity { get; set; }
        public DateTime DeadlineStartDate { get; set; }
        public DateTime DeadlineEndDate { get; set; }
        public Guid IDTrainer { get; set; }
        public string TrainerName { get; set; }
    }
}
