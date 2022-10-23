using System;

namespace Model.Subdomains.ScoreSubdomain.Trainer
{
    public class Candidate
    {        
        public Guid IDCandidate { get; set; }        
        
        public string IDPosition { get; set; }
        
        public string NIM { get; set; }               

        public string Name { get; set; } 
        
        public string PositionName { get; set; }

        public string AssignmentNotes { get; set; }

        public string SubmissionFilePath { get; set; }
    }
}
