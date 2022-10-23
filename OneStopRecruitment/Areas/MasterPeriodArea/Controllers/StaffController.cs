using Microsoft.AspNetCore.Mvc;
using Model.DBConstraint;
using Model.Subdomains.MasterPeriodSubdomain;
using OneStopRecruitment.Areas.MasterPeriodArea.ViewModels;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.AuthenticationHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.RequestHelpers;
using OneStopRecruitment.Models;
using Service.Modules.MasterPeriodModule;
using System;

namespace OneStopRecruitment.Areas.MasterPeriodArea.Controllers
{
    [Area("MasterPeriodArea")]
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
            MasterPeriodViewModel viewModel = new MasterPeriodViewModel();
            viewModel.PeriodList = staffService.GetAllPeriod();
            return View(viewModel);
        }

        public IActionResult UpdateActivePeriod(int selectedPeriod)
        {            
            staffService.UpdateActivePeriod(selectedPeriod);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult InsertPeriod()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MasterPeriodViewModel viewModel = new MasterPeriodViewModel();
            viewModel.Period = new Period();
            viewModel.Period.DeadlineStart = DateTime.Now;
            viewModel.Period.DeadlineEnd = DateTime.Now;
            return View("InsertUpdatePeriod", viewModel);
        }

        [EncryptedActionParameter]
        [HttpGet]
        public IActionResult UpdatePeriod(int PeriodID)
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MasterPeriodViewModel viewModel = new MasterPeriodViewModel();
            viewModel.Period = staffService.GetPeriodByID(PeriodID);
            if(viewModel.Period == null)
            {
                return RedirectToAction("Index");
            }
            return View("InsertUpdatePeriod", viewModel);
        }

        [HttpPost]
        public IActionResult InsertPeriod(Period period)
        {
            MasterPeriodViewModel viewModel = new MasterPeriodViewModel()
            {
                Period = period
            };
            if (!ModelState.IsValid)
            {                    
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdatePeriod", viewModel);
            }

            try
            {
                bool result = staffService.InsertPeriod(period);

                if (result)
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdatePeriod", viewModel);
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdatePeriod", viewModel);
            }                        
        }

        [HttpPost]
        public IActionResult UpdatePeriod(Period period)
        {
            MasterPeriodViewModel viewModel = new MasterPeriodViewModel()
            {
                Period = period
            };
            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdatePeriod", viewModel);
            }

            try
            {
                bool result = staffService.UpdatePeriod(period);

                if (result)
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdatePeriod", viewModel);
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdatePeriod", viewModel);
            }
        }        
    }    
}
