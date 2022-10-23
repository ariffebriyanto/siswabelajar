using Helper.DataAnnotation;
using Model.DBConstraint;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.MasterScoringComponentSubdomain
{
    public class ScoringComponent
    {
        public Guid IDScoringComponent { get; set; }

        [Required(ErrorMessage = AlertConstraint.ScoringComponent.EmptyScoringComponentTypeList)]        
        public Guid? IDScoringComponentType { get; set; }

        [Required(ErrorMessage = AlertConstraint.ScoringComponent.EmptyPeriod)]
        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.ScoringComponent.EmptyPeriod)]
        public int IDPeriod { get; set; }

        [Required(ErrorMessage = AlertConstraint.ScoringComponent.EmptyStage)]
        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.ScoringComponent.EmptyStage)]
        public int IDStage { get; set; }
        
        public int IDSubStage { get; set; }

        [Required(ErrorMessage = AlertConstraint.ScoringComponent.EmptyPosition)]
        public string IDPosition { get; set; }

        [Required(ErrorMessage = AlertConstraint.ScoringComponent.EmptyScoringComponent)]
        public string ScoringComponentText { get; set; }

        [Required(ErrorMessage = AlertConstraint.ScoringComponent.EmptyMinScore)]
        public decimal MinScore { get; set; }

        [Required(ErrorMessage = AlertConstraint.ScoringComponent.EmptyMaxScore)]
        [NumberGreaterThan("MinScore", ErrorMessage = AlertConstraint.ScoringComponent.InvalidMaxScore)]
        public decimal MaxScore { get; set; }

        public string PeriodName { get; set; }
        public string StageName { get; set; }
        public string SubStageName { get; set; }
    }
}
