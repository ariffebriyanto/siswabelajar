using Microsoft.AspNetCore.Mvc;
using Service.Modules;
using System.Threading;
using System.Threading.Tasks;

namespace OneStopRecruitment.Controllers
{
    public class SchedulerController : Controller
    {
        CandidateMailJob CandidateMailJob;
        public SchedulerController(CandidateMailJob candidateMailJob)
        {
            CandidateMailJob = candidateMailJob;
        }
        public async Task<bool> TestAsync(CancellationToken cancellationToken)
        {
            try
            {
                await CandidateMailJob.DoWork(cancellationToken);
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
