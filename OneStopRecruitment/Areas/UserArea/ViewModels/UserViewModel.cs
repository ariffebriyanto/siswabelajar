using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Subdomains.UserSubdomain;
using System;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.UserArea.ViewModels
{
    public class UserViewModel
    {
        public List<SelectListItem> RoleList { get; set; }
        public int IDRole { get; set; }
        public List<User> UserList { get; set; }
        public User User { get; set; }
        public Guid IDUser { get; set; }
        public string SessionUsername { get; set; }
    }
}
