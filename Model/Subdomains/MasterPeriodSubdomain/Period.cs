using Helper.DataAnnotation;
using Model.DBConstraint;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.MasterPeriodSubdomain
{
    public class Period
    {
        public int IDPeriod { get; set; }        
        public int IDStage { get; set; }
        public string StageName { get; set; }
        [Required(ErrorMessage = AlertConstraint.Period.EmptyPeriodName)]
        public string PeriodName { get; set; }                
        public int IsActive { get; set; }
        [Required(ErrorMessage = AlertConstraint.Period.EmptyDeadlineStart)]
        public DateTime DeadlineStart { get; set; }
        [Required(ErrorMessage = AlertConstraint.Period.EmptyDeadlineEnd)]     
        [DateGreaterEqualsThanAttribute("DeadlineStart", ErrorMessage = AlertConstraint.Period.DateNotGreaterThan)]
        public DateTime DeadlineEnd { get; set; }
        public int IsComplete { get; set; }
    }
}
