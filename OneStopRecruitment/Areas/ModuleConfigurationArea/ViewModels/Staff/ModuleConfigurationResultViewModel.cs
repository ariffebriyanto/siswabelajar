using Microsoft.AspNetCore.Mvc.Rendering;
using Model.DBConstraint;
using Model.Subdomains.ModuleConfigurationSubdomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneStopRecruitment.Areas.ModuleConfigurationArea.ViewModels.Staff
{
    public class ModuleConfigurationResultViewModel
    {
        public List<SelectListItem> RoleList { get; set; }
        [Required(ErrorMessage = AlertConstraint.ModuleConfiguration.EmptyRole)]
        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.ModuleConfiguration.EmptyRole)]
        public int IDRole { get; set; }
        public string RoleName { get; set; }
        public List<Module> ModuleList { get; set; }
    }
}
