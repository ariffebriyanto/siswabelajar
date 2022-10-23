using System;

namespace Model.Subdomains.MasterCandidateSubdomain
{
    public class CandidateDraft
    {
        public Guid IDCandidateDraft { get; set; }
        public Guid IDCandidate { get; set; }
        public int IDPeriod { get; set; }
        public string IDPosition { get; set; }
        public int IDStage { get; set; }        
        public int IsPass { get; set; }
        public string Note { get; set; }
    }
}
