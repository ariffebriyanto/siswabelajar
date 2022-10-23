using Model.DBConstraint;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.MinimumScoreSubdomain
{
    public class SubStage
    {
        public int IDSubStage { get; set; }
        [Required(ErrorMessage = AlertConstraint.SubStage.EmptyStage)]
        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.SubStage.EmptyStage)]
        public int IDStage { get; set; }
        [Required(ErrorMessage = AlertConstraint.SubStage.EmptySubStageName)]
        public string SubStageName { get; set; }
        public string StageName { get; set; }
    }
}
