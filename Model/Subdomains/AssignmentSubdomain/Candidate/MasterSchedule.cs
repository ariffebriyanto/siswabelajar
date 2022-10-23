using Helper.DataAnnotation;
using Model.DBConstraint;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.AssignmentSubdomain.Candidate
{
    public class MasterSchedule
    {                
        public Guid IDSchedule { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.Schedule.EmptyPeriod)]
        public int IDPeriod { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.Schedule.EmptyStage)]
        public int IDStage { get; set; }
        
        public int IDSubStage { get; set; }

        [Required(ErrorMessage = AlertConstraint.Schedule.EmptyPosition)]
        public string IDPosition { get;set; }

        [Required(ErrorMessage = AlertConstraint.Schedule.EmptyDate)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = AlertConstraint.Schedule.EmptyStartTime)]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = AlertConstraint.Schedule.EmptyEndTime)]
        [TimeGreaterEqualsThan("StartTime", ErrorMessage = AlertConstraint.Schedule.InvalidEndTime)]
        public TimeSpan EndTime { get; set; }

        [Required(ErrorMessage = AlertConstraint.Schedule.EmptyRoom)]
        public string Room { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.Schedule.EmptyLimit)]
        public int Limit { get; set; }
        public int Qty { get; set; }
        public string PeriodName { get; set; }
        public string StageName { get; set; }
        public string SubStageName { get; set; }
    }
}
