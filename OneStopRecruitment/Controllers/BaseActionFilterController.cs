using Microsoft.AspNetCore.Mvc;
using OneStopRecruitment.Middlewares.ActionFilters;

namespace OneStopRecruitment.Controllers
{
    [ServiceFilter(typeof(SetCurrentMenuOrRedirectActionFilter))]
    [ServiceFilter(typeof(AuthenticationActionFilter))]
    public class BaseActionFilterController : BaseController
    {
    }
}
