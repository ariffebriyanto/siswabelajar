using Helper.DataAnnotation;
using Microsoft.AspNetCore.Http;
using Model.DBConstraint;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.AssignmentSubdomain.Staff
{
    public class Assignment
    {
        public Guid IDSchedule { get; set; }
        public int IDAssignment { get; set; }
        [Required(ErrorMessage = AlertConstraint.Assignment.EmptyPeriod)]
        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.Assignment.EmptyPeriod)]
        public int IDPeriod { get; set; }
        public string PeriodName { get; set; }
        [Required(ErrorMessage = AlertConstraint.Assignment.EmptyStage)]
        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.Assignment.EmptyStage)]
        public int IDStage { get; set; }
        public string StageName { get; set; }
        [Required(ErrorMessage = AlertConstraint.Assignment.EmptySubStage)]
        [RegularExpression(BaseConstraint.Regex.NonMandatoryIntDropdown, ErrorMessage = AlertConstraint.Assignment.EmptySubStage)]
        public int IDSubStage { get; set; }
        public string SubStageName { get; set; }
        [Required(ErrorMessage = AlertConstraint.Assignment.EmptyStartDate)]
        public DateTime? DeadlineStartDate { get; set; }
        [Required(ErrorMessage = AlertConstraint.Assignment.EmptyEndDate)]
        [DateGreaterEqualsThan("DeadlineStartDate", ErrorMessage = AlertConstraint.Assignment.InvalidEndDate)]
        public DateTime? DeadlineEndDate { get; set; }
        public string Room { get; set; }
        [Required(ErrorMessage = AlertConstraint.Assignment.EmptyNotes)]
        public string Notes { get; set; }
        //[Required(ErrorMessage = AlertConstraint.Assignment.EmptyFile)]
        public IFormFile AssignmentFile { get; set; }
        public string AssignmentFileName { get; set; }
        public Guid IDTrainer { get; set; }
        public string TrainerName { get; set; }
        public DateTime ScheduleDate { get; set; }
        public TimeSpan ScheduleStartTime { get; set; }
        public TimeSpan ScheduleEndTime { get; set; }
    }
}
