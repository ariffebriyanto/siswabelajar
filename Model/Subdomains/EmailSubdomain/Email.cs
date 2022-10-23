using Model.DBConstraint;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.EmailSubdomain
{
    public class Email
    {
        public List<string> Recipients { get; set; }
        [Required(ErrorMessage = AlertConstraint.Email.EmptySubject)]
        public string Subject { get; set; }
        [Required(ErrorMessage = AlertConstraint.Email.EmptyBody)]
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}
