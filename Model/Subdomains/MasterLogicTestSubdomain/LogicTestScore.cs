using System;

namespace Model.Subdomains.MasterLogicTestSubdomain
{
    public class LogicTestScore
    {
        public Guid IDLogicTestScore { get; set; }
        public Guid IDCandidate { get; set; }
        public int IDPeriod { get; set; }
        public int Score { get; set; }
    }
}
