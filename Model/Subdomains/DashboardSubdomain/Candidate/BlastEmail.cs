using System;

namespace Model.Subdomains.DashboardSubdomain.Candidate
{
    public class BlastEmail
    {
        public Guid IDBlastEmail { get; set; }        
        public int IDPeriod { get; set; }
        public string Subject { get; set; }        
        public string Description { get; set; }
        public DateTime BlastDateTime { get; set; }
    }
}
