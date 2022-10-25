using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.EmailSubdomain;
using Model.Subdomains.MasterCandidateSubdomain;
using Repository.Repositories.OneStopRecruitmentRepository;
using Service.Helpers.EmailHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Service.Modules
{
    public class CandidateMailJob : CronJobService
    {
        private readonly ILogger<CandidateMailJob> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly MasterScheduleRepository _masterScheduleRepository;
        private readonly PeriodRepository _periodRepository;
        private readonly CandidateRepository _candidateRepository;
        private readonly TransactionScheduleRepository _transactionScheduleRepository;
        private readonly UserRepository _userRepository;
        private readonly IEmailHelper _emailHelper;

        public CandidateMailJob(IScheduleConfig<CandidateMailJob> config, ILogger<CandidateMailJob> logger, IServiceProvider serviceProvider)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _masterScheduleRepository = serviceProvider.GetRequiredService<MasterScheduleRepository>();
            _periodRepository = serviceProvider.GetRequiredService<PeriodRepository>();
            _candidateRepository = serviceProvider.GetRequiredService<CandidateRepository>();
            _transactionScheduleRepository = serviceProvider.GetRequiredService<TransactionScheduleRepository>();
            _userRepository = serviceProvider.GetRequiredService<UserRepository>();
            _emailHelper = serviceProvider.GetRequiredService<IEmailHelper>();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ScheduleEmailJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("{now} ScheduleEmailJob is working.", DateTime.Now.ToString("T"));

            /* Scheduler for MsSchedule with Active Period (7 days) */
            var period = _periodRepository.GetActivePeriod();
            var scheduleDTOs = _masterScheduleRepository.GetScheduleByPeriod(period.IDPeriod)
                .Where(i => i.Date.Date.CompareTo(DateTime.Now.Date) == 7)
                .OrderBy(x => x.Date).ThenBy(x => x.StartTime)
                .ToList();

            foreach (var item in scheduleDTOs)
            {
                /* Get Candidates */
                var candidates = new List<CandidateDTO>();
                var idPositions = item.IDPosition.Split(';').ToArray();
                foreach (var candidate in idPositions)
                    candidates.AddRange(_candidateRepository.GetCandidateByStage(item.IDPeriod, item.IDStage, item.IDPosition).ToList());

                var newCandidates = candidates.Select(i => new { i.NIM, i.Email }).Distinct().ToList();
                var userCandidates = _userRepository.GetUserCandidateList(newCandidates.Select(i => i.NIM).ToList(), 2);
                var trxSchedules = _transactionScheduleRepository.GetByScheduleAndUsers(item.IDSchedule, userCandidates.Select(i => i.IDUser).ToList());
                var unenrolledCandidates = userCandidates.Where(i => trxSchedules.Select(j => j.IDUser).Contains(i.IDUser)).ToList();

                _emailHelper.Send(new Email
                {
                    Recipients = unenrolledCandidates.Select(i => i.Email).Distinct().ToList(),
                    Subject = "Ongoing Schedule",
                    Body = "Ongoing Schedule body",
                    IsBodyHtml = true
                });
            }

            /* Scheduler for MsSchedule with Active Period (1 days) */
            scheduleDTOs = _masterScheduleRepository.GetScheduleByPeriod(period.IDPeriod)
                .Where(i => i.Date.Date.CompareTo(DateTime.Now.Date) == 0)
                .OrderBy(x => x.Date).ThenBy(x => x.StartTime)
                .ToList();

            foreach (var item in scheduleDTOs)
            {
                /* Checking Schedule (End of Group Schedule?) */
                var endGroupScheduleDate = _masterScheduleRepository.GetScheduleByPeriodStageSubStage(item.IDPeriod, item.IDStage, item.IDSubStage)
                    .OrderByDescending(i => i.ScheduleDate)
                    .Select(i => i.ScheduleDate.Date)
                    .FirstOrDefault();
                if (item.Date.Date != endGroupScheduleDate.Date)
                    continue;

                /* Get Candidates */
                var candidates = new List<CandidateDTO>();
                var idPositions = item.IDPosition.Split(';').ToArray();
                foreach (var candidate in idPositions)
                    candidates.AddRange(_candidateRepository.GetCandidateByStage(item.IDPeriod, item.IDStage, item.IDPosition).ToList());

                var newCandidates = candidates.Select(i => new { i.NIM, i.Email }).Distinct().ToList();
                var userCandidates = _userRepository.GetUserCandidateList(newCandidates.Select(i => i.NIM).ToList(), 2);
                var trxSchedules = _transactionScheduleRepository.GetByScheduleAndUsers(item.IDSchedule, userCandidates.Select(i => i.IDUser).ToList());
                var unenrolledCandidates = userCandidates.Where(i => trxSchedules.Select(j => j.IDUser).Contains(i.IDUser)).ToList();

                _emailHelper.Send(new Email
                {
                    Recipients = unenrolledCandidates.Select(i => i.Email).Distinct().ToList(),
                    Subject = "Ongoing Schedule",
                    Body = "Ongoing Schedule body",
                    IsBodyHtml = true
                });
            }

            /* Assignment here */
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ScheduleEmailJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
