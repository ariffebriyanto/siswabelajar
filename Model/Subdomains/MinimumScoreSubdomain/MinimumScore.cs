using Model.DBConstraint;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.MinimumScoreSubdomain
{
    public class MinimumScore
    {
        public int IDMinimumScore { get; set; }

        [Required(ErrorMessage = AlertConstraint.MinimumScore.EmptyPeriod)]
        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.MinimumScore.EmptyPeriod)]
        public int IDPeriod { get; set; }

        [Required(ErrorMessage = AlertConstraint.MinimumScore.EmptyStage)]
        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.MinimumScore.EmptyStage)]
        public int IDStage { get; set; }

        public int IDSubStage { get; set; }

        [Required(ErrorMessage = AlertConstraint.MinimumScore.EmptyMinimumScore)]
        public decimal Score { get; set; }

        public string StageName { get; set; }

        public string SubStageName { get; set; }

        public string PeriodName { get; set; }
    }
}
