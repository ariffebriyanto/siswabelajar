using Helper.RandomHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.DBConstraint;
using Model.Subdomains.DropdownSubdomain;
using Model.Subdomains.UserSubdomain;
using OneStopRecruitment.Areas.UserArea.ViewModels;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.AuthenticationHelpers;
using OneStopRecruitment.Helpers.DropdownHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.RequestHelpers;
using OneStopRecruitment.Models;
using Service.Modules.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.WebPages;

namespace OneStopRecruitment.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    public class StaffController : BaseController
    {
        private readonly IStaffService userService;
        public StaffController(IStaffService userService)
        {
            this.userService = userService;
        }

        public IEnumerable<SelectListItem> GetRoleDropdown()
        {
            List<DropdownItem> roleDropdownList = new List<DropdownItem>();
            var RoleList = userService.GetRoles();
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

        private void GetInitialData(UserViewModel viewModel)
        {
            viewModel.RoleList = GetRoleDropdown().ToList();            
        }

        [HttpGet]
        public IActionResult Index()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            UserViewModel viewModel = new UserViewModel();
            GetInitialData(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(UserViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
            }            

            GetInitialData(viewModel);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult InsertUser()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            UserViewModel viewModel = new UserViewModel();
            viewModel.User = new User();
            viewModel.RoleList = GetRoleDropdown().ToList();
            return View("InsertUpdateUser", viewModel);
        }

        [HttpGet]
        [EncryptedActionParameter]
        public IActionResult UpdateUser(string UserID)
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            UserViewModel viewModel = new UserViewModel();
            viewModel.User = userService.GetUserByID(new Guid(UserID));
            viewModel.RoleList = GetRoleDropdown().ToList();
            return View("InsertUpdateUser", viewModel);
        }

        [HttpGet]
        [EncryptedActionParameter]
        public IActionResult UpdatePassword(string UserID)
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            User user = new User();
            user.IDUser = new Guid(UserID);
            UserViewModel viewModel = new UserViewModel()
            {
                User = user
            };
            return View("UpdatePassword", viewModel);
        }

        private void ValidateInsertNewUser(User user)
        {
            if (user.Password.IsEmpty() || user.Password.Length < 8)
            {
                ModelState.AddModelError("User.Password", AlertConstraint.User.PasswordLength);
            }

            if (user.ConfirmPassword.IsEmpty() || !user.ConfirmPassword.Equals(user.Password))
            {
                ModelState.AddModelError("User.ConfirmPassword", AlertConstraint.User.ConfirmPasswordNotMatch);
            }

            if (user.Username != null && userService.CheckUsername(user.Username))
            {
                ModelState.AddModelError("User.Username", AlertConstraint.User.UsernameExists);
            }
        }

        private void ValidateUpdatePassword(User user)
        {
            ModelState.Clear();
            if (user.OldPassword.IsEmpty() || !userService.CheckPassword(user))
            {
                ModelState.AddModelError("User.OldPassword", AlertConstraint.User.IncorrectOldPassword);
            }

            if (user.Password.IsEmpty() || user.Password.Length < 8)
            {
                ModelState.AddModelError("User.Password", AlertConstraint.User.PasswordLength);
            }

            if (user.ConfirmPassword.IsEmpty() || !user.ConfirmPassword.Equals(user.Password))
            {
                ModelState.AddModelError("User.ConfirmPassword", AlertConstraint.User.ConfirmPasswordNotMatch);
            }
        }

        [HttpPost]
        public IActionResult InsertUser(User user)
        {
            UserViewModel viewModel = new UserViewModel()
            {
                User = user,
                RoleList = GetRoleDropdown().ToList()
            };

            ValidateInsertNewUser(user);

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateUser", viewModel);
            }

            try
            {
                if (userService.InsertUser(user))
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdateUser", viewModel);
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateUser", viewModel);
            }
        }

        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            UserViewModel viewModel = new UserViewModel()
            {
                User = user,
                RoleList = GetRoleDropdown().ToList()
            };            

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateUser", viewModel);
            }

            try
            {
                if (userService.UpdateUser(user))
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdateUser", viewModel);
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateUser", viewModel);
            }
        }

        [HttpPost]
        public IActionResult UpdatePassword(User user)
        {
            UserViewModel viewModel = new UserViewModel()
            {
                User = user                
            };
            
            ValidateUpdatePassword(user);

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("UpdatePassword", viewModel);
            }

            try
            {
                if (userService.UpdatePassword(user))
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("UpdatePassword", viewModel);
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("UpdatePassword", viewModel);
            }
        }

        [HttpPost]
        public IActionResult DeleteUser(Guid UserID)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = BaseConstraint.NotificationType.Failed, message = AlertConstraint.Default.Failed });
            }

            try
            {
                bool result = userService.DeleteUser(UserID);
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

        [HttpPost]
        public IActionResult ResetPassword(Guid UserID)
        {            
            try
            {
                string newPassword = RandomHelper.RandomPass();
                User user = new User()
                {
                    IDUser = UserID,
                    Password = newPassword
                };                

                bool result = userService.UpdatePassword(user);
                if (result == true)
                {
                    return Json(new { status = BaseConstraint.NotificationType.Success, message = AlertConstraint.Default.Success, newPass = newPassword });
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
