using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model.DBConstraint;
using Model.Subdomains.MasterLogicTestSubdomain.Candidate;
using OneStopRecruitment.Areas.MasterLogicTestArea.ViewModels.Candidate;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.DataDirectoryHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.RequestHelpers;
using Service.Modules.MasterLogicTestModule;
using System;

namespace OneStopRecruitment.Areas.MasterLogicTestArea.Controllers
{
    [Area("MasterLogicTestArea")]
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

        public void GetImagePath(MasterLogicTestQuestion question)
        {
            if (question.Type.Equals(BaseConstraint.OptionType.Image))
            {
                question.QuestionImage = fileHelper.GetImagePath(question.QuestionImage, DirectoryConstraint.LOGIC_TEST_QUESTION_IMAGE);
            }

            if (question.FirstChoiceType.Equals(BaseConstraint.OptionType.Image))
            {
                question.FirstChoiceImage = fileHelper.GetImagePath(question.FirstChoiceImage, DirectoryConstraint.LOGIC_TEST_ANSWER_IMAGE);
            }

            if (question.SecondChoiceType.Equals(BaseConstraint.OptionType.Image))
            {
                question.SecondChoiceImage = fileHelper.GetImagePath(question.SecondChoiceImage, DirectoryConstraint.LOGIC_TEST_ANSWER_IMAGE);
            }

            if (question.ThirdChoiceType.Equals(BaseConstraint.OptionType.Image))
            {
                question.ThirdChoiceImage = fileHelper.GetImagePath(question.ThirdChoiceImage, DirectoryConstraint.LOGIC_TEST_ANSWER_IMAGE);
            }

            if (question.FourthChoiceType.Equals(BaseConstraint.OptionType.Image))
            {
                question.FourthChoiceImage = fileHelper.GetImagePath(question.FourthChoiceImage, DirectoryConstraint.LOGIC_TEST_ANSWER_IMAGE);
            }
        }

        private int ValidateTime(MasterSchedule schedule)
        {
            DateTime now = DateTime.Now;

            if (now.Date.CompareTo(schedule.Date.Date) < 0 || (now.Date.CompareTo(schedule.Date.Date) == 0 && now.TimeOfDay.CompareTo(schedule.StartTime) < 0))
            {
                return BaseConstraint.LogicTest.NotStarted;
            }
            else if (schedule.Date.Date.CompareTo(DateTime.Now.Date) == 0 && now.TimeOfDay.CompareTo(schedule.StartTime) >= 0 &&
                now.TimeOfDay.CompareTo(schedule.EndTime) <= 0)
            {
                return BaseConstraint.LogicTest.OnGoing;
            }
            else if (now.Date.CompareTo(schedule.Date.Date) > 0 || (now.Date.CompareTo(schedule.Date.Date) == 0 && now.TimeOfDay.CompareTo(schedule.EndTime) > 0))
            {
                return BaseConstraint.LogicTest.Done;
            }

            return -1;
        }

        [HttpGet]
        [EncryptedActionParameter]
        public IActionResult Index(string ScheduleID)
        {
            LogicTestViewModel viewModel = new LogicTestViewModel();
            Login login = HttpContext.Session.GetLoggedinUser();
            viewModel.QuestionList = candidateService.GetLogicTestByPeriod(login.Username);            
            foreach(var item in viewModel.QuestionList)
            {
                GetImagePath(item);
            }

            viewModel.Schedule = candidateService.GetScheduleByID(new Guid(ScheduleID));            
            viewModel.Status = ValidateTime(viewModel.Schedule);
            if(viewModel.Status == BaseConstraint.LogicTest.OnGoing)
            {
                viewModel.TimeLeft = viewModel.Schedule.EndTime.Subtract(DateTime.Now.TimeOfDay);
            }

            return View(viewModel);
        }

        //[HttpPost]
        //public bool InsertUpdateAnswer(string IDLogicTestQuestion, string Answer)
        //{
        //    Login login = HttpContext.Session.GetLoggedinUser();
        //    return candidateService.InsertUpdateLogicTestAnswer(new Guid(IDLogicTestQuestion), login.Username, Answer);            
        //}

        [HttpPost]
        public IActionResult InsertUpdateAnswer(string IDLogicTestQuestion, string Answer, string IDSchedule)
        {
            MasterSchedule schedule = candidateService.GetScheduleByID(new Guid(IDSchedule));
            int status = ValidateTime(schedule);
            if(status == BaseConstraint.LogicTest.NotStarted)
            {
                return Json(new { status = BaseConstraint.NotificationType.Failed, message = AlertConstraint.LogicTest.LogicTestNotStarted});
            }
            else if (status == BaseConstraint.LogicTest.Done)
            {
                return Json(new { status = BaseConstraint.NotificationType.Failed, message = AlertConstraint.LogicTest.LogicTestDone });
            }
            else
            {
                Login login = HttpContext.Session.GetLoggedinUser();
                candidateService.InsertUpdateLogicTestAnswer(new Guid(IDLogicTestQuestion), login.Username, Answer);
                return Json(new { status = BaseConstraint.NotificationType.Success, message = AlertConstraint.Default.Success });                
            }            
        }
    }
}
