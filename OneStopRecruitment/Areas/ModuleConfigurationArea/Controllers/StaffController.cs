using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.DBConstraint;
using Model.Subdomains.DropdownSubdomain;
using Model.Subdomains.ModuleConfigurationSubdomain;
using OneStopRecruitment.Areas.ModuleConfigurationArea.ViewModels.Staff;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.AuthenticationHelpers;
using OneStopRecruitment.Helpers.DropdownHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.RequestHelpers;
using OneStopRecruitment.Models;
using Service.Modules.ModuleConfigurationModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneStopRecruitment.Areas.ModuleConfigurationArea.Controllers
{
    [Area("ModuleConfigurationArea")]
    public class StaffController : BaseController
    {
        private readonly IStaffService staffService;
        public StaffController(IStaffService staffService)
        {
            this.staffService = staffService;
        }

        #region Dropdown
        public IEnumerable<SelectListItem> GetRoleDropdown()
        {
            List<DropdownItem> roleDropdownList = new List<DropdownItem>();
            var RoleList = staffService.GetRoles();
            foreach (var item in RoleList)
            {
                DropdownItem roleDropdown = new DropdownItem();
                roleDropdown.Value = item.IDRole.ToString();
                roleDropdown.Text = item.RoleName;
                roleDropdownList.Add(roleDropdown);
            }
            IEnumerable<SelectListItem> dropdownRole = new SelectListItemBuilder()
                .AddRangeDropdownItems(roleDropdownList.AsEnumerable())
                .Build();
            return dropdownRole;
        }
        #endregion

        #region Initial Data
        public void GetResultInitialData(ModuleConfigurationResultViewModel moduleConfigurationResultViewModel)
        {
            moduleConfigurationResultViewModel.RoleList = GetRoleDropdown().ToList();
        }

        public void GetFormInitialData(ModuleConfigurationFormViewModel moduleConfigurationFormViewModel)
        {
            moduleConfigurationFormViewModel.RoleList = GetRoleDropdown().ToList();
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);

            ModuleConfigurationResultViewModel moduleConfigurationResultViewModel = new ModuleConfigurationResultViewModel();

            GetResultInitialData(moduleConfigurationResultViewModel);

            return View("Index", moduleConfigurationResultViewModel);
        }

        [HttpPost]
        public IActionResult Index(ModuleConfigurationResultViewModel moduleConfigurationResultViewModel)
        {
            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
            }

            GetResultInitialData(moduleConfigurationResultViewModel);
            return View("Index", moduleConfigurationResultViewModel);
        }

        [HttpGet]
        public IActionResult InsertModule()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);

            ModuleConfigurationFormViewModel moduleConfigurationFormViewModel = new ModuleConfigurationFormViewModel();

            GetFormInitialData(moduleConfigurationFormViewModel);
            moduleConfigurationFormViewModel.ModuleForm = new Module();

            return View("InsertUpdateModule", moduleConfigurationFormViewModel);
        }

        [HttpPost]
        public IActionResult InsertModule(ModuleConfigurationFormViewModel moduleConfigurationFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                GetFormInitialData(moduleConfigurationFormViewModel);
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateModule", moduleConfigurationFormViewModel);
            }

            try
            {
                bool result = staffService.InsertModule(moduleConfigurationFormViewModel.ModuleForm);
                if (result == true)
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    GetFormInitialData(moduleConfigurationFormViewModel);
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdateModule", moduleConfigurationFormViewModel);
                }
            }
            catch
            {
                GetFormInitialData(moduleConfigurationFormViewModel);
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateModule", moduleConfigurationFormViewModel);
            }
        }

        [HttpGet]
        [EncryptedActionParameter]
        public IActionResult UpdateModule(Guid ModuleID)
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);

            ModuleConfigurationFormViewModel moduleConfigurationFormViewModel = new ModuleConfigurationFormViewModel();

            GetFormInitialData(moduleConfigurationFormViewModel);
            moduleConfigurationFormViewModel.ModuleForm = staffService.GetModuleByIdModule(ModuleID);

            return View("InsertUpdateModule", moduleConfigurationFormViewModel);
        }

        [HttpPost]
        public IActionResult UpdateModule(ModuleConfigurationFormViewModel moduleConfigurationFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                GetFormInitialData(moduleConfigurationFormViewModel);
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateModule", moduleConfigurationFormViewModel);
            }

            try
            {
                bool result = staffService.UpdateModule(moduleConfigurationFormViewModel.ModuleForm);
                if (result == true)
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    GetFormInitialData(moduleConfigurationFormViewModel);
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdateModule", moduleConfigurationFormViewModel);
                }
            }
            catch
            {
                GetFormInitialData(moduleConfigurationFormViewModel);
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateModule", moduleConfigurationFormViewModel);
            }
        }

        [HttpPost]
        public IActionResult DeleteModule(Guid ModuleID)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = BaseConstraint.NotificationType.Failed, message = AlertConstraint.Default.Failed });
            }

            try
            {
                bool result = staffService.DeleteModule(ModuleID);
                if (result == true)
                {
                    return Json(new { status = BaseConstraint.NotificationType.Success, message = AlertConstraint.Default.Success });
                }
                else
                {
                    return Json(new { status = BaseConstraint.NotificationType.Failed, message = AlertConstraint.Default.Failed });
                }
            }
            catch
            {
                return Json(new { status = BaseConstraint.NotificationType.Failed, message = AlertConstraint.Default.Error });
            }
        }
    }
}
