using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.DBConstraint;
using Model.Subdomains.AssignmentSubdomain.Staff;
using Model.Subdomains.DropdownSubdomain;
using OneStopRecruitment.Areas.AssignmentArea.ViewModels.Staff;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.AuthenticationHelpers;
using OneStopRecruitment.Helpers.DataDirectoryHelpers;
using OneStopRecruitment.Helpers.DropdownHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.RequestHelpers;
using OneStopRecruitment.Models;
using Service.Modules.AssignmentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneStopRecruitment.Areas.AssignmentArea.Controllers
{
    [Area("AssignmentArea")]
    public class StaffController : BaseController
    {
        private readonly IStaffService staffService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly FileHelper fileHelper;
        public StaffController(
            IStaffService staffService,
            IWebHostEnvironment webHostEnvironment
        )
        {
            this.staffService = staffService;
            this.webHostEnvironment = webHostEnvironment;
            this.fileHelper = new FileHelper(webHostEnvironment);
        }

        #region Dropdown
        public IEnumerable<SelectListItem> GetPeriodDropdown()
        {
            List<DropdownItem> periodDropdownList = new List<DropdownItem>();
            var PeriodList = staffService.GetPeriods();
            foreach (var item in PeriodList)
            {
                DropdownItem periodDropdown = new DropdownItem();
                periodDropdown.Value = item.IDPeriod.ToString();
                periodDropdown.Text = item.PeriodName;
                periodDropdownList.Add(periodDropdown);
            }
            IEnumerable<SelectListItem> dropdownPeriod = new SelectListItemBuilder()
                .AddRangeDropdownItems(periodDropdownList.AsEnumerable())
                .Build();
            return dropdownPeriod;
        }

        public IEnumerable<SelectListItem> GetStageDropdown()
        {
            List<DropdownItem> stageDropdownList = new List<DropdownItem>();
            var StageList = staffService.GetStages();
            foreach (var item in StageList)
            {
                DropdownItem stageDropdown = new DropdownItem();
                stageDropdown.Value = item.IDStage.ToString();
                stageDropdown.Text = item.StageName;
                stageDropdownList.Add(stageDropdown);
            }
            IEnumerable<SelectListItem> dropdownStage = new SelectListItemBuilder()
                .AddRangeDropdownItems(stageDropdownList.AsEnumerable())
                .Build();
            return dropdownStage;
        }

        public IEnumerable<SelectListItem> GetSubStageDropdown(int stageID)
        {
            List<DropdownItem> subStageDropdownList = new List<DropdownItem>();
            var SubStageList = staffService.GetSubStagesByStageId(stageID);
            foreach (var item in SubStageList)
            {
                DropdownItem subStageDropdown = new DropdownItem();
                subStageDropdown.Value = item.IDSubStage.ToString();
                subStageDropdown.Text = item.SubStageName;
                subStageDropdownList.Add(subStageDropdown);
            }
            IEnumerable<SelectListItem> dropdownStage = new SelectListItemBuilder()
                .AddRangeDropdownItems(subStageDropdownList.AsEnumerable())
                .Build();
            return dropdownStage;
        }
        #endregion

        #region Dropdown Based on Schedule
        public IEnumerable<SelectListItem> GetStageInScheduleDropdown(int periodID)
        {
            List<DropdownItem> stageDropdownList = new List<DropdownItem>();
            var StageList = staffService.GetStagesInScheduleNoAssignment(periodID);
            foreach (var item in StageList)
            {
                DropdownItem stageDropdown = new DropdownItem();
                stageDropdown.Value = item.IDStage.ToString();
                stageDropdown.Text = item.StageName;
                stageDropdownList.Add(stageDropdown);
            }
            IEnumerable<SelectListItem> dropdownStage = new SelectListItemBuilder()
                .AddRangeDropdownItems(stageDropdownList.AsEnumerable())
                .Build();
            return dropdownStage;
        }

        public IEnumerable<SelectListItem> GetSubStageInScheduleDropdown(int periodID, int stageID)
        {
            List<DropdownItem> subStageDropdownList = new List<DropdownItem>();
            var SubStageList = staffService.GetSubStagesInScheduleNoAssignment(periodID, stageID);
            foreach (var item in SubStageList)
            {
                DropdownItem subStageDropdown = new DropdownItem();
                subStageDropdown.Value = item.IDSubStage.ToString();
                subStageDropdown.Text = item.SubStageName;
                subStageDropdownList.Add(subStageDropdown);
            }
            IEnumerable<SelectListItem> dropdownStage = new SelectListItemBuilder()
                .AddRangeDropdownItems(subStageDropdownList.AsEnumerable())
                .Build();
            return dropdownStage;
        }
        #endregion

        #region File
        public bool UploadAssignmentQuestionFile(Assignment assignment)
        {
            try
            {
                if (assignment.AssignmentFile.ContentType != null)
                {
                    string fileName = fileHelper.UploadFile(assignment.AssignmentFile, DirectoryConstraint.ASSIGNMENT_QUESTION_FILE);

                    if (fileName != null && !fileName.Trim().Equals(""))
                    {
                        assignment.AssignmentFileName = fileName;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool DeleteAssignmentQuestionFile(Assignment assignment)
        {
            try
            {
                string lastFilePath = staffService.GetAssignmentQuestionFilePathById(assignment.IDAssignment);

                bool result = false;
                if (lastFilePath != null)
                {
                    result = fileHelper.DeleteFile(lastFilePath, DirectoryConstraint.ASSIGNMENT_QUESTION_FILE);
                }

                return result;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IFormFile> LoadAssignmentQuestionFile(string FileName)
        {
            if (FileName == null || FileName == "")
            {
                return null;
            }

            try
            {
                return await fileHelper.LoadFile(FileName, DirectoryConstraint.ASSIGNMENT_QUESTION_FILE);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IActionResult> DownloadAssignmentQuestionFile(string FileName)
        {
            if (FileName == null || FileName == "")
            {
                return Content(AlertConstraint.Default.FileNotFound);
            }

            try
            {
                return await fileHelper.DownloadFile(FileName, DirectoryConstraint.ASSIGNMENT_QUESTION_FILE);
            }
            catch
            {
                return Content(AlertConstraint.Default.FileNotFound);
            }
        }

        public async Task<IActionResult> DownloadSubmissionAnswerFile(string FileName)
        {
            if (FileName == null || FileName == "")
            {
                return Content(AlertConstraint.Default.FileNotFound);
            }

            try
            {
                return await fileHelper.DownloadFile(FileName, DirectoryConstraint.ASSIGNMENT_ANSWER_FILE);
            }
            catch
            {
                return Content(AlertConstraint.Default.FileNotFound);
            }
        }
        #endregion

        #region Initial Data
        public void GetResultInitialData(AssignmentResultViewModel assignmentResultViewModel)
        {
            assignmentResultViewModel.PeriodList = GetPeriodDropdown().ToList();
            assignmentResultViewModel.StageList = GetStageDropdown().ToList();
        }

        public void GetFormInitialData(AssignmentFormViewModel assignmentFormViewModel)
        {
            var currentPeriod = new Period();
            try
            {
                currentPeriod = staffService.GetCurrentPeriod();
            }
            catch
            {
                currentPeriod = null;
            }

            assignmentFormViewModel.AssignmentForm.IDPeriod = currentPeriod.IDPeriod;
            assignmentFormViewModel.AssignmentForm.PeriodName = currentPeriod.PeriodName;
            assignmentFormViewModel.StageList = GetStageInScheduleDropdown(assignmentFormViewModel.AssignmentForm.IDPeriod).ToList();
        }

        public void GetUpdateFormInitialData(AssignmentFormViewModel assignmentFormViewModel)
        {
            var fixedData = staffService.GetAssignmentFixedDataByIDAssignment(assignmentFormViewModel.AssignmentForm.IDAssignment);

            assignmentFormViewModel.AssignmentForm.IDAssignment = fixedData.IDAssignment;
            assignmentFormViewModel.AssignmentForm.AssignmentFileName = fixedData.FilePath;
            assignmentFormViewModel.AssignmentForm.IDPeriod = fixedData.IDPeriod;
            assignmentFormViewModel.AssignmentForm.PeriodName = fixedData.PeriodName;
            assignmentFormViewModel.AssignmentForm.IDStage = fixedData.IDStage;
            assignmentFormViewModel.AssignmentForm.StageName = fixedData.StageName;
            assignmentFormViewModel.AssignmentForm.IDSubStage = fixedData.IDSubStage;
            assignmentFormViewModel.AssignmentForm.SubStageName = fixedData.SubStageName;
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);

            AssignmentResultViewModel assignmentResultViewModel = new AssignmentResultViewModel();

            GetResultInitialData(assignmentResultViewModel);

            return View("Index", assignmentResultViewModel);
        }

        [HttpPost]
        public IActionResult Index(AssignmentResultViewModel assignmentResultViewModel)
        {
            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
            }

            GetResultInitialData(assignmentResultViewModel);
            return View("Index", assignmentResultViewModel);
        }

        [HttpGet]
        public IActionResult InsertAssignment()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            AssignmentFormViewModel assignmentFormViewModel = new AssignmentFormViewModel();
            Assignment assignment = new Assignment();
            assignmentFormViewModel.AssignmentForm = assignment;

            GetFormInitialData(assignmentFormViewModel);

            if (assignmentFormViewModel.AssignmentForm.IDPeriod == 0)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Assignment.NoPeriod));
                return RedirectToAction("Index");
            }

            int countSchedule = staffService.CountScheduleByActivePeriod(assignmentFormViewModel.AssignmentForm.IDPeriod);

            if (countSchedule == 0)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Assignment.NoSchedule));
                return RedirectToAction("Index");
            }

            return View("InsertUpdateAssignment", assignmentFormViewModel);
        }

        [HttpPost]
        public IActionResult InsertAssignment(AssignmentFormViewModel assignmentFormViewModel)
        {
            bool isValid = true;
            if (!ModelState.IsValid)
            {
                isValid = false;                    
            }
            if(assignmentFormViewModel.AssignmentForm.AssignmentFile == null)
            {
                ModelState.AddModelError("AssignmentForm.AssignmentFile", AlertConstraint.Assignment.EmptyFile);
                isValid = false;
            }

            if (!isValid)
            {
                GetFormInitialData(assignmentFormViewModel);
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateAssignment", assignmentFormViewModel);
            }

            try
            {
                bool uploadQuestionFile = UploadAssignmentQuestionFile(assignmentFormViewModel.AssignmentForm);
                if (uploadQuestionFile == true)
                {
                    bool result = staffService.InsertAssignment(assignmentFormViewModel.AssignmentForm);
                    if (result == true)
                    {
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        GetFormInitialData(assignmentFormViewModel);
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                        return View("InsertUpdateAssignment", assignmentFormViewModel);
                    }
                }
                else
                {
                    GetFormInitialData(assignmentFormViewModel);
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.FailedUploadFile));
                    return View("InsertUpdateAssignment", assignmentFormViewModel);
                }
            }
            catch
            {
                GetFormInitialData(assignmentFormViewModel);
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateAssignment", assignmentFormViewModel);
            }
        }

        [HttpGet]
        [EncryptedActionParameter]
        public async Task<IActionResult> UpdateAssignment(int AssignmentID)
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            AssignmentFormViewModel assignmentFormViewModel = new AssignmentFormViewModel();
            Assignment assignment = new Assignment();

            assignmentFormViewModel.AssignmentForm = assignment;
            assignmentFormViewModel.AssignmentForm = staffService.GetAssignmentByIDAssignment(AssignmentID);
            assignmentFormViewModel.AssignmentForm.AssignmentFile = await LoadAssignmentQuestionFile(assignmentFormViewModel.AssignmentForm.AssignmentFileName);

            return View("InsertUpdateAssignment", assignmentFormViewModel);
        }

        [HttpPost]
        public IActionResult UpdateAssignment(AssignmentFormViewModel assignmentFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                GetUpdateFormInitialData(assignmentFormViewModel);
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateAssignment", assignmentFormViewModel);
            }

            try
            {
                bool deleteOldFile = true;
                bool uploadQuestionFile = true;
                if(assignmentFormViewModel.AssignmentForm.AssignmentFile != null)
                {
                    deleteOldFile = DeleteAssignmentQuestionFile(assignmentFormViewModel.AssignmentForm);
                }                

                if (deleteOldFile == true)
                {
                    if(assignmentFormViewModel.AssignmentForm.AssignmentFile != null)
                    {
                        uploadQuestionFile = UploadAssignmentQuestionFile(assignmentFormViewModel.AssignmentForm);
                    }                    

                    if (uploadQuestionFile == true)
                    {
                        bool result = staffService.UpdateAssignment(assignmentFormViewModel.AssignmentForm);
                        if (result == true)
                        {
                            AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            GetUpdateFormInitialData(assignmentFormViewModel);
                            AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                            return View("InsertUpdateAssignment", assignmentFormViewModel);
                        }
                    }
                    else
                    {
                        GetUpdateFormInitialData(assignmentFormViewModel);
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.FailedUploadFile));
                        return View("InsertUpdateAssignment", assignmentFormViewModel);
                    }
                }
                else
                {
                    GetUpdateFormInitialData(assignmentFormViewModel);
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.FailedUploadFile));
                    return View("InsertUpdateAssignment", assignmentFormViewModel);
                }
            }
            catch
            {
                GetUpdateFormInitialData(assignmentFormViewModel);
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateAssignment", assignmentFormViewModel);
            }
        }

        [HttpGet]
        [EncryptedActionParameter]
        public IActionResult AssignmentDetails(int AssignmentID, Guid ScheduleID)
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            AssignmentDetailsViewModel assignmentDetailsViewModel = new AssignmentDetailsViewModel();
            assignmentDetailsViewModel.Schedule = staffService.GetAssignmentDetailsIDAssignmentAndIDSubmission(AssignmentID, ScheduleID);
            assignmentDetailsViewModel.SubmissionList = staffService.GetSubmissionsByIDAssignmentAndIDSubmission(AssignmentID, ScheduleID);
            return View(assignmentDetailsViewModel);
        }
    }
}
