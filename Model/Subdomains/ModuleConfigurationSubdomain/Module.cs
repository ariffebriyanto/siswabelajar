using Model.DBConstraint;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.ModuleConfigurationSubdomain
{
    public class Module
    {
        public Guid IDModule { get; set; }
        [Required(ErrorMessage = AlertConstraint.ModuleConfiguration.EmptyRole)]
        [Range(1, Int32.MaxValue, ErrorMessage =AlertConstraint.ModuleConfiguration.EmptyRole)]
        public int IDRole { get; set; }
        [Required(ErrorMessage = AlertConstraint.ModuleConfiguration.EmptyModule)]
        public string ModuleName { get; set; }
        [Required(ErrorMessage = AlertConstraint.ModuleConfiguration.EmptyModuleLevel)]
        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.ModuleConfiguration.EmptyModuleLevel)]
        public int ModuleLevel { get; set; }
        [Required(ErrorMessage = AlertConstraint.ModuleConfiguration.EmptyRoute)]
        public string Route { get; set; }
    }
}