using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.DBConstraint;
using Model.Subdomains.DropdownSubdomain;
using Model.Subdomains.MinimumScoreSubdomain;
using OneStopRecruitment.Areas.MinimumScoreArea.ViewModels;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.AuthenticationHelpers;
using OneStopRecruitment.Helpers.DropdownHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.RequestHelpers;
using OneStopRecruitment.Models;
using Service.Modules.MinimumScoreModule;
using System.Collections.Generic;
using System.Linq;

namespace OneStopRecruitment.Areas.MinimumScoreArea.Controllers
{
    [Area("MinimumScoreArea")]
    public class StaffController : BaseController
    {
        public readonly IStaffService minimumScoreService;                        

        public StaffController(IStaffService minimumScoreService)
        {
            this.minimumScoreService = minimumScoreService;                                    
        }

        [HttpGet]
        public IActionResult Index()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MinimumScoreViewModel viewModel = new MinimumScoreViewModel();
            viewModel.MinimumScoreList = minimumScoreService.GetAllMinimumScore();
            return View(viewModel);
        }


        public bool DeleteMinimumScore(int IDMinimumScore)
        {
            bool result = minimumScoreService.DeleteMinimumScore(IDMinimumScore);
            return result;
        }

        private IEnumerable<SelectListItem> GetPeriodDropdown()
        {
            List<DropdownItem> roleDropdownList = new List<DropdownItem>();
            var periodList = minimumScoreService.GetAllPeriod();
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
            var stageList = minimumScoreService.GetStages();
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
            var subStageList = minimumScoreService.GetAllSubStage();
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

        public List<SubStage> GetSubStageList(int IDStage)
        {
            return minimumScoreService.GetSubStageByStageID(IDStage);
        }

        [HttpGet]
        public IActionResult InsertMinimumScore()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MinimumScoreViewModel viewModel = new MinimumScoreViewModel();
            viewModel.MinimumScore = new MinimumScore();
            viewModel.PeriodList = GetPeriodDropdown().ToList();
            viewModel.StageList = GetStageDropdown().ToList();
            viewModel.SubStageList = GetSubStageDropdown().ToList();
            return View("InsertUpdateMinimumScore", viewModel);
        }

        [EncryptedActionParameter]
        [HttpGet]        
        public IActionResult UpdateMinimumScore(int MinimumScoreID)
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MinimumScoreViewModel viewModel = new MinimumScoreViewModel();
            viewModel.MinimumScore = minimumScoreService.GetMinimumScoreByID(MinimumScoreID);
            viewModel.PeriodList = GetPeriodDropdown().ToList();
            viewModel.StageList = GetStageDropdown().ToList();
            viewModel.SubStageList = GetSubStageDropdown().ToList();            
            return View("InsertUpdateMinimumScore", viewModel);
        }

        private void ValidateInput(MinimumScore minimumScore)
        {
            List<SubStage> subStageList = minimumScoreService.GetSubStageByStageID(minimumScore.IDStage);
            if(subStageList != null && subStageList.Count > 0 && minimumScore.IDSubStage == 0)
            {
                ModelState.AddModelError("MinimumScore.IDSubStage", AlertConstraint.MinimumScore.EmptySubStage);                
            }            
        }

        [HttpPost]
        public IActionResult InsertMinimumScore(MinimumScore minimumScore)
        {
            MinimumScoreViewModel viewModel = new MinimumScoreViewModel()
            {
                MinimumScore = minimumScore,
                PeriodList = GetPeriodDropdown().ToList(),
                StageList = GetStageDropdown().ToList(),
                SubStageList = GetSubStageDropdown().ToList()
            };

            ValidateInput(minimumScore);

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateMinimumScore", viewModel);
            }

            try
            {
                bool IsExists = minimumScoreService.CheckMinimumScore(minimumScore.IDMinimumScore, minimumScore.IDPeriod, minimumScore.IDStage, minimumScore.IDSubStage);
                if (!IsExists)
                {
                    bool result = minimumScoreService.InsertMinimumScore(minimumScore);
                    if (result)
                    {
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                        return View("InsertUpdateMinimumScore", viewModel);
                    }
                }
                else
                {
                    ModelState.AddModelError("MinimumScore.IDMinimumScore", AlertConstraint.MinimumScore.AlreadyExists);
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdateMinimumScore", viewModel);
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateMinimumScore", viewModel);
            }
        }

        [HttpPost]
        public IActionResult UpdateMinimumScore(MinimumScore minimumScore)
        {
            MinimumScoreViewModel viewModel = new MinimumScoreViewModel()
            {
                MinimumScore = minimumScore,
                PeriodList = GetPeriodDropdown().ToList(),
                StageList = GetStageDropdown().ToList(),
                SubStageList = GetSubStageDropdown().ToList()
            };

            ValidateInput(minimumScore);

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateMinimumScore", viewModel);
            }

            try
            {
                bool IsExists = minimumScoreService.CheckMinimumScore(minimumScore.IDMinimumScore, minimumScore.IDPeriod, minimumScore.IDStage, minimumScore.IDSubStage);
                if (!IsExists)
                {
                    bool result = minimumScoreService.UpdateMinimumScore(minimumScore);
                    if (result)
                    {
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                        return View("InsertUpdateMinimumScore", viewModel);
                    }
                }
                else
                {
                    ModelState.AddModelError("MinimumScore.IDMinimumScore", AlertConstraint.MinimumScore.AlreadyExists);
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdateMinimumScore", viewModel);
                }                
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateMinimumScore", viewModel);
            }
        }
    }
}
