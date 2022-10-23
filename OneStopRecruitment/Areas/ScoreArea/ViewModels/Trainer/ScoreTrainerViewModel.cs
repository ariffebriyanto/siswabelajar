using Model.Subdomains.ScoreSubdomain.Trainer;
using System;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.ScoreArea.ViewModels.Trainer
{
    public class ScoreTrainerViewModel
    {
        public Candidate Candidate { get; set; }
        public List<ScoringComponent> QuestionList { get; set; }
        public Guid IDCandidate { get; set; }
        public Guid IDSchedule { get; set; }
        public int IDAssignment { get; set; }
    }
}
