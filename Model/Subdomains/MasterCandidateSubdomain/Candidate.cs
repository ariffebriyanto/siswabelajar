using System;
using System.Collections.Generic;

namespace Model.Subdomains.MasterCandidateSubdomain
{
    public class Candidate
    {        
        public Guid IDCandidate { get; set; }
        
        public int IDPeriod { get; set; }
        
        public int IDRole { get; set; }
        
        public int IDStage { get; set; }
        
        public string IDPosition { get; set; }
        
        public string NIM { get; set; }
        
        public string Email { get; set; }

        public int IsAccepted { get; set; }

        public decimal GPA { get; set; }

        public int LogicTestScore { get; set; }

        public string Name { get; set; }

        public bool IsPass { get; set; }

        public bool IsMeetCriteria { get; set; }

        public bool IsAlreadySubmit { get; set; }

        public string Password { get; set; }

        public decimal Score { get; set; }

        public string Note { get; set; }
        
        public List<SubStage> SubStageScoreList { get; set; }
    }
}
