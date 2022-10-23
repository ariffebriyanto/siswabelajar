using System;

namespace Model.Subdomains.MasterCandidateSubdomain
{
    public class CandidateScore
    {        
        public Guid IDCandidateScore { get; set; }
        
        public Guid IDCandidate { get; set; }
        
        public Guid IDQuestion { get; set; }
        
        public decimal Score { get; set; }
    }
}
