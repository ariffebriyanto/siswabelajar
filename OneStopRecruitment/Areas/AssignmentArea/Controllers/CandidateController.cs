using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model.DBConstraint;
using Model.Subdomains.AssignmentSubdomain.Candidate;
using OneStopRecruitment.Areas.AssignmentArea.ViewModels.Candidate;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.DataDirectoryHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.RequestHelpers;
using OneStopRecruitment.Helpers.TagHelpers;
using OneStopRecruitment.Models;
using Service.Modules.AssignmentModule;
using System;
using System.Threading.Tasks;

namespace OneStopRecruitment.Areas.AssignmentArea.Controllers
{
    [Area("AssignmentArea")]
    public class CandidateController : BaseController
    {
        private readonly ICandidateService candidateService;
        private readonly FileHelper fileHelper;
        public CandidateController(ICandidateService candidateService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.candidateService = candidateService;
            this.fileHelper = new FileHelper(webHostEnvironment);
        }

        public void GetInitialData(AssignmentCandidateViewModel viewModel, int IDAssignment)
        {
            viewModel.IDAssignment = IDAssignment;
            viewModel.Assignment = candidateService.GetAssignmentByID(IDAssignment);
            viewModel.LastSubmission = candidateService.GetLastSubmission(IDAssignment, HttpContext.Session.GetLoggedinUser().IDUser);
            if(viewModel.LastSubmission != null)
            {
                viewModel.IDSubmission = viewModel.LastSubmission.IDSubmission;
            }
            else
            {
                viewModel.IDSubmission = Guid.Empty;
            }
        }

        public bool UploadAssignmentFile(Submission submission)
        {
            try
            {
                if (submission.AssignmentFile.ContentType != null)
                {
                    string fileName = fileHelper.UploadFile(submission.AssignmentFile, DirectoryConstraint.ASSIGNMENT_ANSWER_FILE);                    

                    if (fileName != null && !fileName.Trim().Equals(""))
                    {
                        submission.FilePath = fileName;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }

        [EncryptedActionParameter]
        [HttpGet]
        public IActionResult Index(int AssignmentID)
        {            
            AssignmentCandidateViewModel viewModel = new AssignmentCandidateViewModel();
            GetInitialData(viewModel, AssignmentID);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult InsertAssignment(AssignmentCandidateViewModel viewModel)
        {
            GetInitialData(viewModel, viewModel.IDAssignment);

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("Index", viewModel);
            }

            try
            {
                if (viewModel.Assignment.DeadlineEndDate < DateTime.Now)
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Assignment.LateSubmission));
                    return View("Index", viewModel);
                }
                else
                {
                    bool uploadFile = UploadAssignmentFile(viewModel.Submission);
                    if (uploadFile)
                    {
                        viewModel.Submission.IDAssignment = viewModel.IDAssignment;
                        viewModel.Submission.IDUser = HttpContext.Session.GetLoggedinUser().IDUser;

                        if (candidateService.InsertSubmission(viewModel.Submission))
                        {
                            AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                            return RedirectToAction("Index", new { assignmentID = Base16.Encode(RouteValueEncryption.Encrypt(viewModel.IDAssignment.ToString())) });
                        }
                        else
                        {
                            AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                            return View("Index", viewModel);
                        }
                    }
                    else
                    {
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.FailedUploadFile));
                        return View("Index", viewModel);
                    }
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.FailedUploadFile));
                return View("Index", viewModel);
            }
        }

        [HttpPost]
        public IActionResult UpdateAssignment(AssignmentCandidateViewModel viewModel)
        {
            GetInitialData(viewModel, viewModel.IDAssignment);

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("Index", viewModel);
            }

            try
            {
                if (viewModel.Assignment.DeadlineEndDate < DateTime.Now)
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Assignment.LateSubmission));
                    return View("Index", viewModel);
                }
                else
                {
                    Submission lastSubmission = candidateService.GetLastSubmission(viewModel.IDAssignment, HttpContext.Session.GetLoggedinUser().IDUser);
                    if (lastSubmission != null)
                    {
                        fileHelper.DeleteFile(lastSubmission.FilePath, DirectoryConstraint.ASSIGNMENT_ANSWER_FILE);
                    }

                    bool uploadFile = UploadAssignmentFile(viewModel.Submission);

                    if (uploadFile)
                    {
                        viewModel.Submission.IDAssignment = viewModel.IDAssignment;
                        viewModel.Submission.IDUser = HttpContext.Session.GetLoggedinUser().IDUser;
                        viewModel.Submission.IDSubmission = viewModel.IDSubmission;

                        if (candidateService.UpdateSubmission(viewModel.Submission))
                        {
                            AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                            return RedirectToAction("Index", new { assignmentID = Base16.Encode(RouteValueEncryption.Encrypt(viewModel.IDAssignment.ToString())) });
                        }
                        else
                        {
                            AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                            return View("Index", viewModel);
                        }
                    }
                    else
                    {
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.FailedUploadFile));
                        return View("Index", viewModel);
                    }
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.FailedUploadFile));
                return View("Index", viewModel);
            }
        }

        [EncryptedActionParameter]
        [HttpGet]
        public async Task<IActionResult> DownloadPreviousSubmission(string FilePath)
        {
            return await fileHelper.DownloadFile(FilePath, DirectoryConstraint.ASSIGNMENT_ANSWER_FILE);
        }

        [EncryptedActionParameter]
        [HttpGet]
        public async Task<IActionResult> DownloadQuestion(string FilePath)
        {
            return await fileHelper.DownloadFile(FilePath, DirectoryConstraint.ASSIGNMENT_QUESTION_FILE);
        }
    }
}
