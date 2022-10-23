using Microsoft.AspNetCore.Http;
using Repository.Context;

namespace OneStopRecruitment.Helpers.HttpExtensions
{
    public class ApplicationSessionDataAccessor : IApplicationSessionDataAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ApplicationSessionDataAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetLoginUserName()
        {
            var result = httpContextAccessor.HttpContext.Session.GetLoggedinUser();
            if (result != null && result.Username != null)
            {
                return result.Username;
            }

            return "Unknown";            
        }
    }
}
