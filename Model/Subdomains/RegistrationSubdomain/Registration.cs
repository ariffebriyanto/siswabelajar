using Model.DBConstraint;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.RegistrationSubdomain
{
    public class Registration
    {
        [Required(ErrorMessage = AlertConstraint.Registration.EmptyNIM)]
        public string NIM { get; set; }

        [Required(ErrorMessage = AlertConstraint.Registration.EmptyEmail)]
        [EmailAddress(ErrorMessage = AlertConstraint.Registration.WrongEmailFormat)]
        public string Email { get; set; }

        [Required(ErrorMessage = AlertConstraint.Registration.EmptyPosition)]
        public string IDPosition { get; set; }
    }
}