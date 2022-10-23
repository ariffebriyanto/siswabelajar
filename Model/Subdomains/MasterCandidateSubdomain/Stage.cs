using Model.DBConstraint;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.MasterCandidateSubdomain
{
    public class Stage
    {
        public int IDStage { get; set; }
        [Required(ErrorMessage = AlertConstraint.Stage.EmptyStageName)]
        public string StageName { get; set; }
        [Required(ErrorMessage = AlertConstraint.Stage.EmptyStageLevel)]
        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.Stage.EmptyStageLevel)]
        public int StageLevel { get; set; }
    }
}
