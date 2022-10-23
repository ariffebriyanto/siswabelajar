using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Subdomains.ModuleConfigurationSubdomain;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.ModuleConfigurationArea.ViewModels.Staff
{
    public class ModuleConfigurationFormViewModel
    {
        public List<SelectListItem> RoleList { get; set; }
        public Module ModuleForm { get; set; }
    }
}
