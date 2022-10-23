using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.DBConstraint;
using Model.Subdomains.DropdownSubdomain;
using Model.Subdomains.SubStageSubdomain;
using OneStopRecruitment.Areas.SubstageArea.ViewModels;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.AuthenticationHelpers;
using OneStopRecruitment.Helpers.DropdownHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Models;
using Service.Modules.SubStageModule;
using System.Collections.Generic;
using System.Linq;

namespace OneStopRecruitment.Areas.SubstageArea.Controllers
{
    [Area("SubstageArea")]
    public class StaffController : BaseController
    {
        private readonly IStaffService subStageService;        
        public StaffController(IStaffService subStageService)
        {
            this.subStageService = subStageService;            
        }

        [HttpGet]
        public IActionResult Index()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            SubstageViewModel viewModel = new SubstageViewModel();
            viewModel.SubStageList = subStageService.GetAllSubStage();
            return View(viewModel);
        }

        private IEnumerable<SelectListItem> GetStageDropdown()
        {
            List<DropdownItem> roleDropdownList = new List<DropdownItem>();
            var stageList = subStageService.GetStages();
            foreach (var item in stageList)
            {
                DropdownItem roleDropdown = new DropdownItem();
                roleDropdown.Value = item.IDStage.ToString();
                roleDropdown.Text = item.StageName;
                roleDropdownList.Add(roleDropdown);
            }
            IEnumerable<SelectListItem> result = new SelectListItemBuilder()
                .AddRangeDropdownItems(roleDropdownList.AsEnumerable())
                .Build();
            return result;
        }

        [HttpGet]
        public IActionResult InsertSubstage()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            SubstageViewModel viewModel = new SubstageViewModel();
            viewModel.SubStage = new SubStage();
            viewModel.StageList = GetStageDropdown().ToList();
            return View("InsertUpdateSubstage", viewModel);
        }

        [HttpGet]
        public IActionResult UpdateSubstage(int SubStageID)
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            SubstageViewModel viewModel = new SubstageViewModel();
            viewModel.SubStage = subStageService.GetSubStageByID(SubStageID);
            viewModel.StageList = GetStageDropdown().ToList();
            return View("InsertUpdateSubstage", viewModel);
        }

        [HttpPost]
        public IActionResult InsertSubstage(SubStage subStage)
        {
            SubstageViewModel viewModel = new SubstageViewModel();
            viewModel.SubStage = subStage;
            viewModel.StageList = GetStageDropdown().ToList();

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateSubstage", viewModel);
            }

            try
            {
                bool result = subStageService.InsertSubStage(subStage);
                if(result)
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                    return View("InsertUpdateSubstage", viewModel);
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateSubstage", viewModel);
            }            
        }

        [HttpPost]
        public IActionResult UpdateSubstage(SubStage subStage)
        {
            SubstageViewModel viewModel = new SubstageViewModel();
            viewModel.SubStage = subStage;
            viewModel.StageList = GetStageDropdown().ToList();

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateSubstage", viewModel);
            }

            try
            {
                bool result = subStageService.UpdateSubStage(subStage);
                if (result)
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                    return View("InsertUpdateSubstage", viewModel);
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateSubstage", viewModel);
            }
        }
    }
}
