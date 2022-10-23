using Model.DBConstraint;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.UserSubdomain
{
    public class User
    {
        public Guid IDUser { get; set; }
        [Required(ErrorMessage = AlertConstraint.User.EmptyName)]
        public string Name { get; set; }

        [Required(ErrorMessage = AlertConstraint.User.EmptyUsername)]
        public string Username { get; set; }

        [Required(ErrorMessage = AlertConstraint.User.EmptyEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage = AlertConstraint.User.EmptyRole)]
        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.User.EmptyRole)]
        public int IDRole { get; set; }
        public string RoleName { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}