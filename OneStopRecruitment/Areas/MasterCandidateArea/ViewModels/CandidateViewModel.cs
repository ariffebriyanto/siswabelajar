using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Subdomains.MasterCandidateSubdomain;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.MasterCandidateArea.ViewModels
{
    public class CandidateViewModel
    {        
        public List<Candidate> CandidateList { get; set; }
        public List<SelectListItem> PeriodList { get; set; }
        public List<SelectListItem> StageList { get; set; }
        public List<SelectListItem> PositionList { get; set; }
        public int IDPeriod { get; set; }
        public int IDStage { get; set; }
        public string IDPosition { get; set; }
        public bool CanSubmitCandidate { get; set; }            
        public int IsSaveAsDraft { get; set; }
        public List<SubStage> SubStageList { get; set; }
    }
}
