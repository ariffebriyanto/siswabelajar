using Microsoft.AspNetCore.Http;
using Model.DBConstraint;
using OneStopRecruitment.Helpers.HttpExtensions;
using System;

namespace OneStopRecruitment.Helpers.AuthenticationHelpers
{
    public class RoleAuthenticator
    {
        private RoleAuthenticator() { }
        public static void AuthenticateRoleArea (Login user, int roleID)
        {
            if (user.IDRole != roleID) 
            {
                throw new Exception(AlertConstraint.User.UnauthorizedAccess);
            }
        }
    }
}
