using System;

namespace Model.Subdomains.AssignmentSubdomain.Trainer
{
    public class CandidateAssignment
    {        
        public Guid IDCandidate { get; set; }        
        public int IDPeriod { get; set; }        
        public int IDRole { get; set; }        
        public int IDStage { get; set; }        
        public string IDPosition { get; set; }        
        public string NIM { get; set; }
        public string Name { get; set; }
        public bool ScoringStatus { get; set; }
        public string SubmissionFilePath { get; set; }        
    }
}
