using Microsoft.AspNetCore.Mvc.Rendering;
using Model.DBConstraint;
using Model.Subdomains.AssignmentSubdomain.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneStopRecruitment.Areas.AssignmentArea.ViewModels.Staff
{
    public class AssignmentResultViewModel
    {
        public int IDAssignment { get; set; }
        public List<SelectListItem> PeriodList { get; set; }
        [Required(ErrorMessage = AlertConstraint.Assignment.EmptyPeriod)]
        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.Assignment.EmptyPeriod)]
        public int IDPeriod { get; set; }
        public List<SelectListItem> StageList { get; set; }
        [Required(ErrorMessage = AlertConstraint.Assignment.EmptyStage)]
        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.Assignment.EmptyStage)]
        public int IDStage { get; set; }
        public List<SelectListItem> SubStageList { get; set; }
        [Required(ErrorMessage = AlertConstraint.Assignment.EmptySubStage)]
        [RegularExpression(BaseConstraint.Regex.NonMandatoryIntDropdown, ErrorMessage = AlertConstraint.Assignment.EmptySubStage)]
        public int IDSubStage { get; set; }
        public List<AssignmentSchedule> AssignmentScheduleList { get; set; }
    }
}
