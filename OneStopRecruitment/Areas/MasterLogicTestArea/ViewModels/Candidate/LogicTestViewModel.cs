using Model.Subdomains.MasterLogicTestSubdomain.Candidate;
using System;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.MasterLogicTestArea.ViewModels.Candidate
{
    public class LogicTestViewModel
    {
        public List<MasterLogicTestQuestion> QuestionList { get; set; }
        public MasterSchedule Schedule { get; set; }
        public TimeSpan TimeLeft { get; set; }
        public int Status { get; set; }
    }
}
