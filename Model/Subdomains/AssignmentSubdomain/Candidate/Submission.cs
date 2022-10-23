using Microsoft.AspNetCore.Http;
using Model.DBConstraint;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.AssignmentSubdomain.Candidate
{
    public class Submission
    {
        public Guid IDSubmission { get; set; }        
        public int IDAssignment { get; set; }
        public Guid IDUser { get; set; }
        public string Notes { get; set; }
        public string FilePath { get; set; }
        public DateTime LastSubmit { get; set; }

        [Required(ErrorMessage = AlertConstraint.Assignment.EmptyFile)]
        public IFormFile AssignmentFile { get; set; }
    }
}
