using System;

namespace Model.Subdomains.AssignmentSubdomain.Staff
{
    public class Submission
    {
        public int IDAssignment { get; set; }
        public Guid IDSubmission { get; set; }
        public Guid IDUser { get; set; }
        public string NIM { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
    }
}
