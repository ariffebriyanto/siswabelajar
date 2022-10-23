using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Subdomains.MasterLogicTestSubdomain;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.MasterLogicTestArea.ViewModels.Staff
{
    public class MasterLogicTestViewModel
    {
        public LogicTestQuestionType LogicTestQuestionType;

        public List<SelectListItem> LogicTestQuestionTypeList;

        public MasterLogicTestQuestion MasterLogicTestQuestion;

        public List<MasterLogicTestQuestion> MasterLogicTestQuestionList;

        public List<Period> PeriodList;

        public int IDPeriod;

        public int TotalPickedQuestion;
    }
}
