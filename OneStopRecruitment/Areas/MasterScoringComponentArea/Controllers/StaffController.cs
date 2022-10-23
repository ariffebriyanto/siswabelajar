using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.DBConstraint;
using Model.Subdomains.DropdownSubdomain;
using Model.Subdomains.MasterScoringComponentSubdomain;
using OneStopRecruitment.Areas.MasterScoringComponentArea.ViewModels.Staff;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.AuthenticationHelpers;
using OneStopRecruitment.Helpers.DropdownHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.RequestHelpers;
using OneStopRecruitment.Models;
using Service.Modules.MasterScoringComponentModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneStopRecruitment.Areas.MasterScoringComponentArea.Controllers
{
    [Area("MasterScoringComponentArea")]
    public class StaffController : BaseController
    {
        private readonly IStaffService staffService;                
        public StaffController(IStaffService staffService)
        {
            this.staffService = staffService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MasterScoringComponentViewModel viewModel = new MasterScoringComponentViewModel();
            viewModel.ScoringComponentList = staffService.GetAllScoringComponent();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult InsertScoringComponentType()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MasterScoringComponentViewModel viewModel = new MasterScoringComponentViewModel();
            viewModel.ScoringComponentType = new ScoringComponentType();
            return View("InsertScoringComponentType", viewModel);
        }

        [HttpPost]
        public IActionResult InsertScoringComponentType(ScoringComponentType scoringComponentType)
        {
            MasterScoringComponentViewModel viewModel = new MasterScoringComponentViewModel()
            {
                ScoringComponentType = scoringComponentType
            };

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertScoringComponentType", viewModel);
            }

            try
            {
                int search = staffService.SearchScoringComponentType(scoringComponentType.ScoringComponentTypeText);
                switch (search)
                {
                    case 1:
                        ModelState.AddModelError("ScoringComponentType.ScoringComponentTypeText", AlertConstraint.ScoringComponent.ExistScoringComponentType);
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                        return View("InsertScoringComponentType", viewModel);
                    case -1:
                        ModelState.AddModelError("ScoringComponentType.ScoringComponentTypeText", AlertConstraint.Default.Error);
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                        return View("InsertScoringComponentType", viewModel);
                    default:
                        bool result = staffService.InsertScoringComponentType(scoringComponentType);
                        if (result)
                        {
                            AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                            return View("InsertScoringComponentType", viewModel);
                        }
                }
            }
            catch (Exception)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertScoringComponentType", viewModel);
            }
        }

        private IEnumerable<SelectListItem> GetPeriodDropdown()
        {
            List<DropdownItem> roleDropdownList = new List<DropdownItem>();
            var periodList = staffService.GetAllPeriod();
            foreach (var item in periodList)
            {
                DropdownItem roleDropdown = new DropdownItem();
                roleDropdown.Value = item.IDPeriod.ToString();
                roleDropdown.Text = item.PeriodName;
                roleDropdownList.Add(roleDropdown);
            }
            IEnumerable<SelectListItem> result = new SelectListItemBuilder()
                .AddRangeDropdownItems(roleDropdownList.AsEnumerable())
                .Build();
            return result;
        }

        private IEnumerable<SelectListItem> GetStageDropdown()
        {
            List<DropdownItem> roleDropdownList = new List<DropdownItem>();
            var stageList = staffService.GetStages();
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

        private IEnumerable<SelectListItem> GetSubStageDropdown()
        {
            List<DropdownItem> roleDropdownList = new List<DropdownItem>();
            var subStageList = staffService.GetAllSubStage();
            foreach (var item in subStageList)
            {
                DropdownItem roleDropdown = new DropdownItem();
                roleDropdown.Value = item.IDSubStage.ToString();
                roleDropdown.Text = item.SubStageName;
                roleDropdownList.Add(roleDropdown);
            }
            IEnumerable<SelectListItem> result = new SelectListItemBuilder()
                .AddRangeDropdownItems(roleDropdownList.AsEnumerable())
                .Build();
            return result;
        }

        private IEnumerable<SelectListItem> GetScoringComponentTypeDropdown()
        {
            List<DropdownItem> roleDropdownList = new List<DropdownItem>();
            var subStageList = staffService.GetAllScoringComponentType();
            foreach (var item in subStageList)
            {
                DropdownItem roleDropdown = new DropdownItem();
                roleDropdown.Value = item.IDScoringComponentType.ToString();
                roleDropdown.Text = item.ScoringComponentTypeText;
                roleDropdownList.Add(roleDropdown);
            }
            IEnumerable<SelectListItem> result = new SelectListItemBuilder()
                .AddRangeDropdownItems(roleDropdownList.AsEnumerable())
                .Build();
            return result;
        }

        public List<SubStage> GetSubStageList(int IDStage)
        {
            return staffService.GetSubStageByStageID(IDStage);
        }

        [HttpGet]
        public IActionResult InsertScoringComponent()
        {
            MasterScoringComponentViewModel viewModel = new MasterScoringComponentViewModel()
            {
                ScoringComponent = new ScoringComponent(),
                PeriodList = GetPeriodDropdown().ToList(),
                StageList = GetStageDropdown().ToList(),
                SubStageList = GetSubStageDropdown().ToList(),
                ScoringComponentTypeList = GetScoringComponentTypeDropdown().ToList(),
                PositionList = staffService.GetPositions()
            };            
            return View("InsertUpdateScoringComponent", viewModel);
        }

        [EncryptedActionParameter]
        [HttpGet]
        public IActionResult UpdateScoringComponent(string ScoringComponentID)
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MasterScoringComponentViewModel viewModel = new MasterScoringComponentViewModel()
            {
                ScoringComponent = staffService.GetScoringComponentByID(new Guid(ScoringComponentID)),
                PeriodList = GetPeriodDropdown().ToList(),
                StageList = GetStageDropdown().ToList(),
                SubStageList = GetSubStageDropdown().ToList(),
                ScoringComponentTypeList = GetScoringComponentTypeDropdown().ToList(),
                PositionList = staffService.GetPositions()
            };
            return View("InsertUpdateScoringComponent", viewModel);
        }


        [HttpPost]
        public bool DeleteScoringComponent(string IDScoringComponent)
        {
            return staffService.DeleteScoringComponent(new Guid(IDScoringComponent));
        }

        private void ValidateInput(ScoringComponent scoringComponent)
        {
            List<SubStage> subStageList = staffService.GetSubStageByStageID(scoringComponent.IDStage);
            if (subStageList != null && subStageList.Count > 0 && scoringComponent.IDSubStage == 0)
            {
                ModelState.AddModelError("ScoringComponent.IDSubStage", AlertConstraint.ScoringComponent.EmptySubStage);
            }
        }


        [HttpPost]
        public IActionResult InsertScoringComponent(ScoringComponent scoringComponent)
        {
            MasterScoringComponentViewModel viewModel = new MasterScoringComponentViewModel()
            {
                ScoringComponent = scoringComponent,
                PeriodList = GetPeriodDropdown().ToList(),
                StageList = GetStageDropdown().ToList(),
                SubStageList = GetSubStageDropdown().ToList(),
                ScoringComponentTypeList = GetScoringComponentTypeDropdown().ToList(),
                PositionList = staffService.GetPositions()
            };

            ValidateInput(scoringComponent);

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateScoringComponent", viewModel);
            }

            try
            {
                bool result = staffService.InsertScoringComponent(scoringComponent);
                if(result)
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdateScoringComponent", viewModel);
                }                
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateScoringComponent", viewModel);
            }            
        }


        [HttpPost]
        public IActionResult UpdateScoringComponent(ScoringComponent scoringComponent)
        {
            MasterScoringComponentViewModel viewModel = new MasterScoringComponentViewModel()
            {
                ScoringComponent = scoringComponent,
                PeriodList = GetPeriodDropdown().ToList(),
                StageList = GetStageDropdown().ToList(),
                SubStageList = GetSubStageDropdown().ToList(),
                ScoringComponentTypeList = GetScoringComponentTypeDropdown().ToList(),
                PositionList = staffService.GetPositions()
            };

            ValidateInput(scoringComponent);

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateScoringComponent", viewModel);
            }

            try
            {
                bool result = staffService.UpdateScoringComponent(scoringComponent);
                if (result)
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdateScoringComponent", viewModel);
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateScoringComponent", viewModel);
            }
        }
    }
}
