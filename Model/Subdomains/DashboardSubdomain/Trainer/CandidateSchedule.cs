using System;

namespace Model.Subdomains.DashboardSubdomain.Trainer
{
    public class CandidateSchedule
    {        
        public Guid IDCandidate { get; set; }
        
        public int IDPeriod { get; set; }
        
        public int IDRole { get; set; }
        
        public int IDStage { get; set; }
        
        public string IDPosition { get; set; }

        public Guid IDSchedule { get; set; }
        
        public string NIM { get; set; }
        public string Name { get; set; }
        public string PositionName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Room { get; set; }
    }
}
