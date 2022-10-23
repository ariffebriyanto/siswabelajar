using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace OneStopRecruitment.Helpers.HttpExtensions
{
    public class Login
    {
        public Guid IDUser { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int IDRole { get; set; }
        public string RoleName { get; set; }
    }
    public static class ApplicationSession
    {
        private static readonly string LOGGEDIN_USER = "LOGGEDIN_USER";

        public static Login GetLoggedinUser(this ISession session)
        {
            string SerializedLoggedInUserData = session.GetString(LOGGEDIN_USER);
            if (SerializedLoggedInUserData == null || string.IsNullOrWhiteSpace(SerializedLoggedInUserData))
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<Login>(SerializedLoggedInUserData);
            }
        }
        public static void SetLoggedinUser(this ISession session, Login login) => session.SetString(LOGGEDIN_USER, JsonConvert.SerializeObject(login));
    }
}
