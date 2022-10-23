using Model.DBConstraint;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.LoginSubdomain
{
    public class Login
    {
        [Required(ErrorMessage = AlertConstraint.Login.EmptyUsername)]
        public string Username { get; set; }

        [Required(ErrorMessage = AlertConstraint.Login.EmptyPassword)]
        public string Password { get; set; }
    }
}