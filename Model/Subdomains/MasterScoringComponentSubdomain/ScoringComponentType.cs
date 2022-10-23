using Model.DBConstraint;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.MasterScoringComponentSubdomain
{
    public class ScoringComponentType
    {
        public Guid IDScoringComponentType { get; set; }

        [Required(ErrorMessage = AlertConstraint.ScoringComponent.EmptyScoringComponentType)]
        public string ScoringComponentTypeText { get; set; }
    }
}
