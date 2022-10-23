using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.DBConstraint;
using Model.Subdomains.DropdownSubdomain;
using OneStopRecruitment.Areas.MasterCandidateArea.ViewModels;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.AuthenticationHelpers;
using OneStopRecruitment.Helpers.DropdownHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.RequestHelpers;
using OneStopRecruitment.Models;
using Service.Modules.MasterCandidateModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneStopRecruitment.Areas.MasterCandidateArea.Controllers
{
    [Area("MasterCandidateArea")]
    public class StaffController : BaseController
    {
        private readonly IStaffService candidateService;                        

        public StaffController(IStaffService candidateService)
        {
            this.candidateService = candidateService;
        }

        #region Dropdown
        private IEnumerable<SelectListItem> GetPeriodDropdown()
        {
            List<DropdownItem> roleDropdownList = new List<DropdownItem>();
            var periodList = candidateService.GetAllPeriod();
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
            var stageList = candidateService.GetStages();
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

        private IEnumerable<SelectListItem> GetPositionDropdown()
        {
            List<DropdownItem> roleDropdownList = new List<DropdownItem>();
            var positionList = candidateService.GetPositions();
            foreach (var item in positionList)
            {
                DropdownItem roleDropdown = new DropdownItem();
                roleDropdown.Value = item.IDPosition;
                roleDropdown.Text = item.PositionName;
                roleDropdownList.Add(roleDropdown);
            }
            IEnumerable<SelectListItem> result = new SelectListItemBuilder()
                .AddRangeDropdownItems(roleDropdownList.AsEnumerable())
                .Build();
            return result;
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            CandidateViewModel viewModel = new CandidateViewModel();            
            viewModel.PeriodList = GetPeriodDropdown().ToList();
            viewModel.StageList = GetStageDropdown().ToList();
            viewModel.PositionList = GetPositionDropdown().ToList();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(CandidateViewModel viewModel)
        {
            viewModel.PeriodList = GetPeriodDropdown().ToList();
            viewModel.StageList = GetStageDropdown().ToList();
            viewModel.PositionList = GetPositionDropdown().ToList();
            return View("Index", viewModel);
        }

        public IActionResult SubmitCandidate(CandidateViewModel viewModel)
        {            
            try
            {
                viewModel.PeriodList = GetPeriodDropdown().ToList();
                viewModel.StageList = GetStageDropdown().ToList();
                viewModel.PositionList = GetPositionDropdown().ToList();

                if (viewModel.IsSaveAsDraft == 1)
                {
                    if (candidateService.SaveCandidateDraft(viewModel.CandidateList.Where(x => x.IsAlreadySubmit == false).ToList()))
                    {
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                        return View("Index", viewModel);
                    }
                }
                else
                {
                    if (candidateService.SubmitCandidate(viewModel.CandidateList, viewModel.IDStage))
                    {
                        if (viewModel.IDStage == 1)
                        {
                            // Pass Screening Test, Generate User
                            bool generateCandidateUser = candidateService.GenereateCandidateUser(viewModel.CandidateList);
                            if (!generateCandidateUser)
                            {
                                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                                return View("Index", viewModel);
                            }
                        }

                        // TODO: Sent Email To All Candidate using CandidateList object 
                        // (it already contains the password for screening test email)
                        bool blastCandidateStatus = candidateService.BlastCandidateStatus(viewModel.CandidateList, viewModel.IDPeriod, viewModel.IDStage);                        

                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                        return View("Index", viewModel);
                    }
                }                
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("Index", viewModel);
            }            
        }

        [HttpPost]
        public IActionResult UpdateToNextStage(int StageID)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = BaseConstraint.NotificationType.Failed, message = AlertConstraint.Default.Failed });
            }

            try
            {
                bool result = candidateService.UpdateToNextStage(StageID);
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
