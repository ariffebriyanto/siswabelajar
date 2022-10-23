using Helper.DataAnnotation;
using Model.DBConstraint;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.ScoreSubdomain.Trainer
{
    public class ScoringComponent
    {
        public Guid IDScoringComponent { get; set; }        
        public Guid? IDScoringComponentType { get; set; }  
        public string IDPosition { get; set; }        
        public string ScoringComponentText { get; set; }
        public string ScoringComponentType { get; set; }
        public decimal MinScore { get; set; }        
        public decimal MaxScore { get; set; }

        [Required(ErrorMessage = AlertConstraint.ScoringComponent.EmptyScore)]
        [NumberGreaterEqualsThan("MinScore", ErrorMessage = AlertConstraint.ScoringComponent.InvalidScore)]
        [NumberLowerEqualsThan("MaxScore", ErrorMessage = AlertConstraint.ScoringComponent.InvalidScore)]
        public decimal Score { get; set; }
        public string Notes { get; set; }
    }
}
