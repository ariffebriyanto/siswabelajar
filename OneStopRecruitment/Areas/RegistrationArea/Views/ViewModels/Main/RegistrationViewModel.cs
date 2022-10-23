using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Subdomains.RegistrationSubdomain;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.RegistrationArea.ViewModels.Main
{
    public class RegistrationViewModel
    {
        public List<SelectListItem> PositionList { get; set; }
        public Registration Registration { get; set; }
    }
}
