using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.DBConstraint;
using Model.Subdomains.DropdownSubdomain;
using Model.Subdomains.MasterLogicTestSubdomain;
using OneStopRecruitment.Areas.MasterLogicTestArea.ViewModels.Staff;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.AuthenticationHelpers;
using OneStopRecruitment.Helpers.DataDirectoryHelpers;
using OneStopRecruitment.Helpers.DropdownHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.RequestHelpers;
using OneStopRecruitment.Models;
using Service.Modules.MasterLogicTestModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneStopRecruitment.Areas.MasterLogicTestArea.Controllers
{
    [Area("MasterLogicTestArea")]
    public class StaffController : BaseController
    {
        private readonly IStaffService staffService;        
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly FileHelper fileHelper;

        public StaffController(IStaffService staffService,             
            IWebHostEnvironment webHostEnvironment)
        {
            this.staffService = staffService;            
            this.webHostEnvironment = webHostEnvironment;
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
        [HttpGet]
        public IActionResult Index()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            List<MasterLogicTestQuestion> questionList = staffService.GetAllLogicTestQuestion();
            MasterLogicTestViewModel viewModel = new MasterLogicTestViewModel();
            foreach(var item in questionList)
            {
                GetImagePath(item);
            }            
            viewModel.MasterLogicTestQuestionList = questionList;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult InsertQuestionType()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MasterLogicTestViewModel viewModel = new MasterLogicTestViewModel();
            viewModel.LogicTestQuestionType = new LogicTestQuestionType();
            return View("InsertQuestionType", viewModel);
        }

        public IEnumerable<SelectListItem> GetQuestionTypeDropdown()
        {
            List<DropdownItem> questionTypeDropdownList = new List<DropdownItem>();
            var questionTypeList = staffService.GetAllLogicTestQuestionType();
            foreach (var item in questionTypeList)
            {
                DropdownItem roleDropdown = new DropdownItem();
                roleDropdown.Value = item.IDLogicTestQuestionType.ToString();
                roleDropdown.Text = item.QuestionType;
                questionTypeDropdownList.Add(roleDropdown);
            }
            IEnumerable<SelectListItem> dropdownQuestionType = new SelectListItemBuilder()
                .AddRangeDropdownItems(questionTypeDropdownList.AsEnumerable())
                .Build();
            return dropdownQuestionType;
        }

        [HttpGet]
        public IActionResult InsertQuestion()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MasterLogicTestViewModel viewModel = new MasterLogicTestViewModel();
            viewModel.MasterLogicTestQuestion = new MasterLogicTestQuestion();
            viewModel.LogicTestQuestionTypeList = GetQuestionTypeDropdown().ToList();
            return View("InsertUpdateQuestion", viewModel);
        }

        [EncryptedActionParameter]
        [HttpGet]
        public IActionResult UpdateQuestion(string LogicTestQuestionID)
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MasterLogicTestViewModel viewModel = new MasterLogicTestViewModel();
            viewModel.LogicTestQuestionTypeList = GetQuestionTypeDropdown().ToList();
            MasterLogicTestQuestion question = staffService.GetQuestionByID(new Guid(LogicTestQuestionID));
            TempData["QuestionImagePath"] = $"{fileHelper.GetStaticDirectory(DirectoryConstraint.LOGIC_TEST_QUESTION_IMAGE)}/";
            TempData["AnswerImagePath"] = $"{fileHelper.GetStaticDirectory(DirectoryConstraint.LOGIC_TEST_ANSWER_IMAGE)}/";
            viewModel.MasterLogicTestQuestion = question;            
            return View("InsertUpdateQuestion", viewModel);
        }        

        [HttpPost]
        public IActionResult InsertQuestionType(LogicTestQuestionType logicTestQuestionType)
        {
            MasterLogicTestViewModel viewModel = new MasterLogicTestViewModel()
            {
                LogicTestQuestionType = logicTestQuestionType
            };

            if (!ModelState.IsValid)
            {                
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertQuestionType", viewModel);
            }

            try
            {
                int search = staffService.SearchQuestionType(logicTestQuestionType.QuestionType);
                switch (search)
                {
                    case 1:
                        ModelState.AddModelError("LogicTestQuestionType.QuestionType", AlertConstraint.LogicTest.ExistQuestionType);
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                        return View("InsertQuestionType", viewModel);                        
                    case -1:
                        ModelState.AddModelError("LogicTestQuestionType.QuestionType", AlertConstraint.Default.Error);
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                        return View("InsertQuestionType", viewModel);
                    default:
                        bool result = staffService.InsertQuestionType(logicTestQuestionType);
                        if (result)
                        {
                            AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                            return View("InsertQuestionType", viewModel);
                        }                        
                }
            }
            catch (Exception)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertQuestionType", viewModel);
            }            
        }
        
        public bool DeleteQuestion(string IDLogicTestQuestion)
        {
            bool result = staffService.DeleteQuestion(IDLogicTestQuestion);            
            return result;
        }

        public bool SaveLogicTestImage(MasterLogicTestQuestion masterLogicTestQuestion)
        {
            try
            {                
                if (masterLogicTestQuestion.Type.Equals(BaseConstraint.OptionType.Image) && masterLogicTestQuestion.QuestionFile != null)
                {
                    string fileName = fileHelper.UploadFile(masterLogicTestQuestion.QuestionFile, DirectoryConstraint.LOGIC_TEST_QUESTION_IMAGE);
                    masterLogicTestQuestion.QuestionImage = fileName;
                }

                if (masterLogicTestQuestion.FirstChoiceType.Equals(BaseConstraint.OptionType.Image) && masterLogicTestQuestion.FirstChoiceFile != null)
                {
                    string fileName = fileHelper.UploadFile(masterLogicTestQuestion.FirstChoiceFile, DirectoryConstraint.LOGIC_TEST_ANSWER_IMAGE);
                    masterLogicTestQuestion.FirstChoiceImage = fileName;
                }

                if (masterLogicTestQuestion.SecondChoiceType.Equals(BaseConstraint.OptionType.Image) && masterLogicTestQuestion.SecondChoiceFile != null)
                {
                    string fileName = fileHelper.UploadFile(masterLogicTestQuestion.SecondChoiceFile, DirectoryConstraint.LOGIC_TEST_ANSWER_IMAGE);
                    masterLogicTestQuestion.SecondChoiceImage = fileName;
                }

                if (masterLogicTestQuestion.ThirdChoiceType.Equals(BaseConstraint.OptionType.Image) && masterLogicTestQuestion.ThirdChoiceFile != null)
                {
                    string fileName = fileHelper.UploadFile(masterLogicTestQuestion.ThirdChoiceFile, DirectoryConstraint.LOGIC_TEST_ANSWER_IMAGE);
                    masterLogicTestQuestion.ThirdChoiceImage = fileName;
                }

                if (masterLogicTestQuestion.FourthChoiceType.Equals(BaseConstraint.OptionType.Image) && masterLogicTestQuestion.FourthChoiceFile != null)
                {
                    string fileName = fileHelper.UploadFile(masterLogicTestQuestion.FourthChoiceFile, DirectoryConstraint.LOGIC_TEST_ANSWER_IMAGE);
                    masterLogicTestQuestion.FourthChoiceImage = fileName;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        [HttpPost]
        public IActionResult InsertQuestion(MasterLogicTestQuestion masterLogicTestQuestion)
        {            
            MasterLogicTestViewModel viewModel = new MasterLogicTestViewModel();
            viewModel.LogicTestQuestionTypeList = GetQuestionTypeDropdown().ToList();
            viewModel.MasterLogicTestQuestion = masterLogicTestQuestion;            

            GetValidationQuestionMessage(masterLogicTestQuestion);
            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateQuestion", viewModel);
            }

            try
            {
                bool saveImage = SaveLogicTestImage(masterLogicTestQuestion);
                if (saveImage)
                {
                    bool result = staffService.InsertQuestion(masterLogicTestQuestion);

                    if (result)
                    {
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                        return View("InsertUpdateQuestion", viewModel);
                    }
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdateQuestion", viewModel);
                }
            }
            catch (Exception)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateQuestion", viewModel);
            }
        }

        [HttpPost]
        public IActionResult UpdateQuestion(MasterLogicTestQuestion masterLogicTestQuestion)
        {
            TempData["QuestionImagePath"] = $"{fileHelper.GetStaticDirectory(DirectoryConstraint.LOGIC_TEST_QUESTION_IMAGE)}/";
            TempData["AnswerImagePath"] = $"{fileHelper.GetStaticDirectory(DirectoryConstraint.LOGIC_TEST_ANSWER_IMAGE)}/";
            MasterLogicTestViewModel viewModel = new MasterLogicTestViewModel();
            viewModel.LogicTestQuestionTypeList = GetQuestionTypeDropdown().ToList();
            viewModel.MasterLogicTestQuestion = masterLogicTestQuestion;

            GetValidationUpdateQuestionMessage(masterLogicTestQuestion);            
            if (!ModelState.IsValid)
            {                
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateQuestion", viewModel);
            }

            try
            {
                bool saveImage = SaveLogicTestImage(masterLogicTestQuestion);
                if (saveImage)
                {
                    bool result = staffService.UpdateQuestion(masterLogicTestQuestion);

                    if (result)
                    {
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                        return View("InsertUpdateQuestion", viewModel);
                    }
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdateQuestion", viewModel);
                }
            }
            catch (Exception)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateQuestion", viewModel);
            }            
        }

        public void GetValidationQuestionMessage(MasterLogicTestQuestion masterLogicTestQuestion)
        {
            if(masterLogicTestQuestion.Type.Equals(BaseConstraint.OptionType.Text) && String.IsNullOrEmpty(masterLogicTestQuestion.QuestionText))
            {
                ModelState.AddModelError("MasterLogicTestQuestion.QuestionText", AlertConstraint.LogicTest.EmptyQuestion);                
            }
            else if(masterLogicTestQuestion.Type.Equals(BaseConstraint.OptionType.Image) && masterLogicTestQuestion.QuestionFile == null)
            {
                ModelState.AddModelError("MasterLogicTestQuestion.QuestionFile", AlertConstraint.LogicTest.EmptyQuestionUpload);                
            }

            if (masterLogicTestQuestion.FirstChoiceType.Equals(BaseConstraint.OptionType.Text) && String.IsNullOrEmpty(masterLogicTestQuestion.FirstChoiceText))
            {
                ModelState.AddModelError("MasterLogicTestQuestion.FirstChoiceText", AlertConstraint.LogicTest.EmptyOptionA);                
            }
            else if (masterLogicTestQuestion.FirstChoiceType.Equals(BaseConstraint.OptionType.Image) && masterLogicTestQuestion.FirstChoiceFile == null)
            {
                ModelState.AddModelError("MasterLogicTestQuestion.FirstChoiceFile", AlertConstraint.LogicTest.EmptyOptionAUpload);                
            }

            if (masterLogicTestQuestion.SecondChoiceType.Equals(BaseConstraint.OptionType.Text) && String.IsNullOrEmpty(masterLogicTestQuestion.SecondChoiceText))
            {
                ModelState.AddModelError("MasterLogicTestQuestion.SecondChoiceText", AlertConstraint.LogicTest.EmptyOptionB);                
            }
            else if (masterLogicTestQuestion.SecondChoiceType.Equals(BaseConstraint.OptionType.Image) && masterLogicTestQuestion.SecondChoiceFile == null)
            {
                ModelState.AddModelError("MasterLogicTestQuestion.SecondChoiceFile", AlertConstraint.LogicTest.EmptyOptionBUpload);                
            }

            if (masterLogicTestQuestion.ThirdChoiceType.Equals(BaseConstraint.OptionType.Text) && String.IsNullOrEmpty(masterLogicTestQuestion.ThirdChoiceText))
            {
                ModelState.AddModelError("MasterLogicTestQuestion.ThirdChoiceText", AlertConstraint.LogicTest.EmptyOptionC);                
            }
            else if (masterLogicTestQuestion.ThirdChoiceType.Equals(BaseConstraint.OptionType.Image) && masterLogicTestQuestion.ThirdChoiceFile == null)
            {
                ModelState.AddModelError("MasterLogicTestQuestion.ThirdChoiceFile", AlertConstraint.LogicTest.EmptyOptionCUpload);                
            }


            if (masterLogicTestQuestion.FourthChoiceType.Equals(BaseConstraint.OptionType.Text) && String.IsNullOrEmpty(masterLogicTestQuestion.FourthChoiceText))
            {
                ModelState.AddModelError("MasterLogicTestQuestion.FourthChoiceText", AlertConstraint.LogicTest.EmptyOptionD);                
            }
            else if (masterLogicTestQuestion.FourthChoiceType.Equals(BaseConstraint.OptionType.Image) && masterLogicTestQuestion.FourthChoiceFile == null)
            {
                ModelState.AddModelError("MasterLogicTestQuestion.FourthChoiceFile", AlertConstraint.LogicTest.EmptyOptionDUpload);                
            }

            if (String.IsNullOrEmpty(masterLogicTestQuestion.CorrectChoice))
            {
                ModelState.AddModelError("MasterLogicTestQuestion.CorrectChoice", AlertConstraint.LogicTest.EmptyAnswer);                
            }
        }


        public void GetValidationUpdateQuestionMessage(MasterLogicTestQuestion masterLogicTestQuestion)
        {
            if (masterLogicTestQuestion.Type.Equals(BaseConstraint.OptionType.Text) && String.IsNullOrEmpty(masterLogicTestQuestion.QuestionText))
            {
                ModelState.AddModelError("MasterLogicTestQuestion.QuestionText", AlertConstraint.LogicTest.EmptyQuestion);
            }
            else if (masterLogicTestQuestion.Type.Equals(BaseConstraint.OptionType.Image) && String.IsNullOrEmpty(masterLogicTestQuestion.QuestionImage) && masterLogicTestQuestion.QuestionFile == null)
            {
                ModelState.AddModelError("MasterLogicTestQuestion.QuestionFile", AlertConstraint.LogicTest.EmptyQuestionUpload);
            }

            if (masterLogicTestQuestion.FirstChoiceType.Equals(BaseConstraint.OptionType.Text) && String.IsNullOrEmpty(masterLogicTestQuestion.FirstChoiceText))
            {
                ModelState.AddModelError("MasterLogicTestQuestion.FirstChoiceText", AlertConstraint.LogicTest.EmptyOptionA);
            }
            else if (masterLogicTestQuestion.FirstChoiceType.Equals(BaseConstraint.OptionType.Image) && String.IsNullOrEmpty(masterLogicTestQuestion.FirstChoiceImage) && masterLogicTestQuestion.FirstChoiceFile == null)
            {
                ModelState.AddModelError("MasterLogicTestQuestion.FirstChoiceFile", AlertConstraint.LogicTest.EmptyOptionAUpload);
            }

            if (masterLogicTestQuestion.SecondChoiceType.Equals(BaseConstraint.OptionType.Text) && String.IsNullOrEmpty(masterLogicTestQuestion.SecondChoiceText))
            {
                ModelState.AddModelError("MasterLogicTestQuestion.SecondChoiceText", AlertConstraint.LogicTest.EmptyOptionB);
            }
            else if (masterLogicTestQuestion.SecondChoiceType.Equals(BaseConstraint.OptionType.Image) && String.IsNullOrEmpty(masterLogicTestQuestion.SecondChoiceImage) && masterLogicTestQuestion.SecondChoiceFile == null)
            {
                ModelState.AddModelError("MasterLogicTestQuestion.SecondChoiceFile", AlertConstraint.LogicTest.EmptyOptionBUpload);
            }

            if (masterLogicTestQuestion.ThirdChoiceType.Equals(BaseConstraint.OptionType.Text) && String.IsNullOrEmpty(masterLogicTestQuestion.ThirdChoiceText))
            {
                ModelState.AddModelError("MasterLogicTestQuestion.ThirdChoiceText", AlertConstraint.LogicTest.EmptyOptionC);
            }
            else if (masterLogicTestQuestion.ThirdChoiceType.Equals(BaseConstraint.OptionType.Image) && String.IsNullOrEmpty(masterLogicTestQuestion.ThirdChoiceImage) && masterLogicTestQuestion.ThirdChoiceFile == null)
            {
                ModelState.AddModelError("MasterLogicTestQuestion.ThirdChoiceFile", AlertConstraint.LogicTest.EmptyOptionCUpload);
            }


            if (masterLogicTestQuestion.FourthChoiceType.Equals(BaseConstraint.OptionType.Text) && String.IsNullOrEmpty(masterLogicTestQuestion.FourthChoiceText))
            {
                ModelState.AddModelError("MasterLogicTestQuestion.FourthChoiceText", AlertConstraint.LogicTest.EmptyOptionD);
            }
            else if (masterLogicTestQuestion.FourthChoiceType.Equals(BaseConstraint.OptionType.Image) && String.IsNullOrEmpty(masterLogicTestQuestion.FourthChoiceImage) && masterLogicTestQuestion.FourthChoiceFile == null)
            {
                ModelState.AddModelError("MasterLogicTestQuestion.FourthChoiceFile", AlertConstraint.LogicTest.EmptyOptionDUpload);
            }

            if (String.IsNullOrEmpty(masterLogicTestQuestion.CorrectChoice))
            {
                ModelState.AddModelError("MasterLogicTestQuestion.CorrectChoice", AlertConstraint.LogicTest.EmptyAnswer);
            }
        }

        #region Pick Question Region
        [HttpGet]
        public IActionResult ViewPickQuestion()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MasterLogicTestViewModel viewModel = new MasterLogicTestViewModel();
            viewModel.PeriodList = staffService.GetAllPeriod();
            return View("PickQuestion", viewModel);
        }

        [HttpPost]
        public IActionResult ViewPickQuestion(int IDPeriod)
        {
            MasterLogicTestViewModel viewModel = new MasterLogicTestViewModel();
            viewModel.IDPeriod = IDPeriod;
            viewModel.PeriodList = staffService.GetAllPeriod();
            return View("PickQuestion", viewModel);
        }

        public bool QuestionChecked(bool IsChecked, string IDLogicTestQuestion, int IDPeriod)
        {
            return staffService.CheckedQuestion(IsChecked, IDLogicTestQuestion, IDPeriod);            
        }
        #endregion Pick Question Region
    }
}
