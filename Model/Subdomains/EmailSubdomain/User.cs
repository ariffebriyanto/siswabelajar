using System;

namespace Model.Subdomains.EmailSubdomain
{
    public class User
    {
        public Guid IDUser { get; set; }

        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int IDRole { get; set; }
        public string RoleName { get; set; }

        
    }
}
