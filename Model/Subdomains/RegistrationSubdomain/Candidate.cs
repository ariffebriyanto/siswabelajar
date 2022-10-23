using System;

namespace Model.Subdomains.RegistrationSubdomain
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
        public decimal GPA { get; set; }
        public int LogicTestScore { get; set; }
        public string Name { get; set; }
        public bool IsPass { get; set; }
        public string Password { get; set; }
    }
}
