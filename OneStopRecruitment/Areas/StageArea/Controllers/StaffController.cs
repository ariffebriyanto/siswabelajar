using Microsoft.AspNetCore.Mvc;
using Model.DBConstraint;
using Model.Subdomains.StageSubdomain;
using OneStopRecruitment.Areas.StageArea.ViewModels.Staff;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.AuthenticationHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.RequestHelpers;
using OneStopRecruitment.Models;
using Service.Modules.StageModule;

namespace OneStopRecruitment.Areas.StageArea.Controllers
{
    [Area("StageArea")]
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
            StageResultViewModel stageResultViewModel = new StageResultViewModel();
            stageResultViewModel.StageList = staffService.GetStages();
            return View(stageResultViewModel);
        }

        [HttpGet]
        public IActionResult InsertStage()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);

            StageFormViewModel stageFormViewModel = new StageFormViewModel();

            stageFormViewModel.StageForm = new Model.Subdomains.StageSubdomain.Stage();

            return View("InsertUpdateStage", stageFormViewModel);
        }

        [HttpPost]
        public IActionResult InsertStage(StageFormViewModel stageFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateStage", stageFormViewModel);
            }

            try
            {
                Stage stage = new Stage();
                stage.StageName = stageFormViewModel.StageForm.StageName;
                stage.StageLevel = stageFormViewModel.StageForm.StageLevel;

                bool result = staffService.InsertStage(stage);
                if (result == true)
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdateStage", stageFormViewModel);
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateStage", stageFormViewModel);
            }
        }

        [HttpGet]
        [EncryptedActionParameter]
        public IActionResult UpdateStage(int StageID)
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);

            StageFormViewModel stageFormViewModel = new StageFormViewModel();

            stageFormViewModel.StageForm = staffService.GetStageById(StageID);

            return View("InsertUpdateStage", stageFormViewModel);
        }

        [HttpPost]
        public IActionResult UpdateStage(StageFormViewModel stageFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateStage", stageFormViewModel);
            }

            try
            {
                Stage stage = new Stage();
                stage.IDStage = stageFormViewModel.StageForm.IDStage;
                stage.StageName = stageFormViewModel.StageForm.StageName;
                stage.StageLevel = stageFormViewModel.StageForm.StageLevel;

                bool result = staffService.UpdateStage(stage);
                if (result == true)
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdateStage", stageFormViewModel);
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateStage", stageFormViewModel);
            }
        }
    }
}
