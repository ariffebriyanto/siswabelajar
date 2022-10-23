using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model.DBConstraint;
using Model.Subdomains.MasterLogicTestSubdomain;
using OneStopRecruitment.Areas.MasterLogicTestArea.ViewModels.Staff;
using OneStopRecruitment.Helpers.DataDirectoryHelpers;
using OneStopRecruitment.Helpers.ViewComponentHelpers;
using Service.Modules.MasterLogicTestModule;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneStopRecruitment.Areas.MasterLogicTestArea.ViewComponents.Staff
{
    [ViewComponent(Name = "TablePickedQuestion")]
    public class TablePickedQuestion : ViewComponent
    {
        private readonly IStaffService staffService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly FileHelper fileHelper;
        public TablePickedQuestion(IStaffService staffService, IWebHostEnvironment webHostEnvironment)
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

        public async Task<IViewComponentResult> InvokeAsync(int IDPeriod)
        {
            MasterLogicTestViewModel viewModel = new MasterLogicTestViewModel();
            viewModel.IDPeriod = IDPeriod;
            List<MasterLogicTestQuestion> listQuestion = staffService.GetPickQuestionList(IDPeriod);
            foreach (var item in listQuestion)
            {
                GetImagePath(item);
            }
            viewModel.MasterLogicTestQuestionList = listQuestion;            
            viewModel.TotalPickedQuestion = viewModel.MasterLogicTestQuestionList.Where(x => x.IsPicked == true).ToList().Count();
            return View(ViewComponentPath.AreaViewPath("MasterLogicTestArea", "Staff", "_TablePickedQuestion"), viewModel);
        }
    }
}
