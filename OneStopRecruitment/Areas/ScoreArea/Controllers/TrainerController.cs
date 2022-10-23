using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model.DBConstraint;
using OneStopRecruitment.Areas.ScoreArea.ViewModels.Trainer;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.AuthenticationHelpers;
using OneStopRecruitment.Helpers.DataDirectoryHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.RequestHelpers;
using OneStopRecruitment.Helpers.TagHelpers;
using OneStopRecruitment.Models;
using Service.Modules.ScoreModule;
using System;
using System.Threading.Tasks;

namespace OneStopRecruitment.Areas.ScoreArea.Controllers
{
    [Area("ScoreArea")]
    public class TrainerController : BaseController
    {
        private readonly ITrainerService trainerService;
        private readonly FileHelper fileHelper;

        public TrainerController(ITrainerService trainerService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.trainerService = trainerService;
            this.fileHelper = new FileHelper(webHostEnvironment);
        }

        [EncryptedActionParameter]
        [HttpGet]
        public IActionResult Index(Guid CandidateID, Guid ScheduleID, int AssignmentID = -1)
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Trainer.Id);
            ScoreTrainerViewModel viewModel = new ScoreTrainerViewModel();
            viewModel.Candidate = trainerService.GetCandidateData(CandidateID, AssignmentID);
            viewModel.QuestionList = trainerService.GetQuestionList(CandidateID, AssignmentID);
            viewModel.IDCandidate = CandidateID;
            viewModel.IDAssignment = AssignmentID;
            viewModel.IDSchedule = ScheduleID;
            return View(viewModel);
        }

        [EncryptedActionParameter]
        [HttpGet]
        public async Task<IActionResult> DownloadSubmission(string FilePath)
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Trainer.Id);
            return await fileHelper.DownloadFile(FilePath, DirectoryConstraint.ASSIGNMENT_ANSWER_FILE);
        }

        [HttpPost]
        public IActionResult Index(ScoreTrainerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("Index", viewModel);
            }

            try
            {
                Guid IDUser = HttpContext.Session.GetLoggedinUser().IDUser;
                if (trainerService.SaveCandidateScore(viewModel.QuestionList, viewModel.IDCandidate, viewModel.IDSchedule, IDUser))
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index", new { candidateID = Base16.Encode(RouteValueEncryption.Encrypt(viewModel.IDCandidate.ToString())),
                        scheduleID = Base16.Encode(RouteValueEncryption.Encrypt(viewModel.IDSchedule.ToString())),
                        assignmentID = Base16.Encode(RouteValueEncryption.Encrypt(viewModel.IDAssignment.ToString())) });
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("Index", viewModel);
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("Index", viewModel);
            }
            
        }
    }
}
