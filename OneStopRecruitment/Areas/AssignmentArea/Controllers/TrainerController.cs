using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model.DBConstraint;
using OneStopRecruitment.Areas.AssignmentArea.ViewModels.Trainer;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.DataDirectoryHelpers;
using OneStopRecruitment.Helpers.RequestHelpers;
using Service.Modules.AssignmentModule;
using System;
using System.Threading.Tasks;

namespace OneStopRecruitment.Areas.AssignmentArea.Controllers
{
    [Area("AssignmentArea")]
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
        public IActionResult Index(int AssignmentID, Guid ScheduleID)
        {
            AssignmentTrainerViewModel viewModel = new AssignmentTrainerViewModel();
            viewModel.Schedule = trainerService.GetScheduleDetail(ScheduleID, AssignmentID);
            viewModel.Assignment = trainerService.GetAssignment(AssignmentID);
            viewModel.CandidateList = trainerService.GetCandidateList(ScheduleID, AssignmentID);
            return View(viewModel);
        }

        [EncryptedActionParameter]
        [HttpGet]
        public async Task<IActionResult> DownloadQuestion(string FilePath)
        {
            return await fileHelper.DownloadFile(FilePath, DirectoryConstraint.ASSIGNMENT_QUESTION_FILE);
        }

        [EncryptedActionParameter]
        [HttpGet]
        public async Task<IActionResult> DownloadSubmission(string FilePath)
        {
            return await fileHelper.DownloadFile(FilePath, DirectoryConstraint.ASSIGNMENT_ANSWER_FILE);
        }
    }
}
