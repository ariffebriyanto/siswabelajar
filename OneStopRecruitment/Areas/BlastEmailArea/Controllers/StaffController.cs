using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.DBConstraint;
using Model.Subdomains.DropdownSubdomain;
using OneStopRecruitment.Areas.BlastEmailArea.ViewModels.Staff;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.AuthenticationHelpers;
using OneStopRecruitment.Helpers.DropdownHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Models;
using Service.Modules.BlastEmailModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneStopRecruitment.Areas.BlastEmailArea.Controllers
{
    [Area("BlastEmailArea")]
    public class StaffController : BaseController
    {
        private readonly IStaffService staffService;
        public StaffController(IStaffService staffService)
        {
            this.staffService = staffService;
        }

        #region Dropdown
        public IEnumerable<SelectListItem> GetUserMultiselectDropdown()
        {
            List<DropdownItem> userMultiselectDropdownList = new List<DropdownItem>();
            var UserList = staffService.GetUserProfileForBlastEmail();
            foreach (var item in UserList)
            {
                DropdownItem userDropdown = new DropdownItem();
                userDropdown.Value = item.Email;
                userDropdown.Text = item.Name + " (" + item.Username + ") - " + item.RoleName;
                userMultiselectDropdownList.Add(userDropdown);
            }
            IEnumerable<SelectListItem> multiselectDropdownUser = new SelectListItemBuilder()
                .AddRangeDropdownItems(userMultiselectDropdownList.AsEnumerable())
                .Build();
            return multiselectDropdownUser;
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            return View();

        }

        [HttpPost]
        public IActionResult Index(BlastEmailViewModel blastEmailViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var subject = blastEmailViewModel.EmailForm.Subject;
            var body = blastEmailViewModel.EmailForm.Body;

            var result = staffService.SendBlastingEmail(subject, body);
            if (result == true)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                return RedirectToAction("Index");
            }
            else
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                return RedirectToAction("Index");
            }
        }
    }
}
