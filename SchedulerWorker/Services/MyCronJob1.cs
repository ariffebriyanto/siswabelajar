using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Model.Subdomains.DashboardSubdomain.Candidate;

using Service.Modules.DashboardModule;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Areas.DashboardArea.ViewModels.Candidate;

namespace SchedulerWorker.Services
{
    public class MyCronJob1 : CronJobService
    {
        private readonly ILogger<MyCronJob1> _logger;
        private readonly ICandidateService candidateService;
       
        public HttpContext HttpContext { get; set; }
       
        public ISession session;
       
           
        


        public MyCronJob1(IScheduleConfig<MyCronJob1> config, ILogger<MyCronJob1> logger)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
          

            
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CronJob 1 starts.");
            return base.StartAsync(cancellationToken);
        }

       

       

        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("{now} CronJob 1 is working.", DateTime.Now.ToString("T"));

           
            DashboardCandidateViewModel viewModel = new DashboardCandidateViewModel();


            if (HttpContext != null)
            {


                Login Login = HttpContext.Session.GetLoggedinUser();

                if (Login != null)
                {
                    if (Login.Username != null)
                    {
                        Dashboard dashboard = new Dashboard();
                        dashboard.CurrentPeriod = candidateService.GetCurrentPeriod(Login.Username);
                        dashboard.CurrentStage = candidateService.GetCurrentStage(Login.Username);
                        viewModel.Dashboard = dashboard;

                        viewModel.ScheduleList = candidateService.GetAvailableSchedules(Login.Username, Login.IDUser);
                        viewModel.CandidateScheduleList = candidateService.GetCandidateSchedules(Login.IDUser);
                        viewModel.AssignmentList = candidateService.GetAvailableAssignments(Login.Username);
                        viewModel.BlastEmailList = candidateService.GetNotifications();
                        viewModel.ToDoList = new List<ToDo>();

                        List<Assignment> unsubmittedAssignment = candidateService.GetUnsubmittedAssignments(Login.Username, Login.IDUser);
                        foreach (var item in unsubmittedAssignment)
                        {
                            if (item.DeadlineEndDate > DateTime.Now)
                            {
                                ToDo newToDo = new ToDo()
                                {
                                    ScheduleorAssignment = "Assignment",
                                    PeriodName = item.PeriodName,
                                    StageName = item.StageName,
                                    SubStageName = item.SubStageName,
                                    DeadlineStartDate = (DateTime)item.DeadlineStartDate,
                                    DeadlineEndDate = (DateTime)item.DeadlineEndDate
                                };
                                viewModel.ToDoList.Add(newToDo);
                            }
                        }

                        List<MasterSchedule> unregisteredSchedule = candidateService.GetUnenrolledSchedules(Login.Username, Login.IDUser);
                        foreach (var item in unregisteredSchedule)
                        {
                            if (item.Date > DateTime.Now)
                            {
                                ToDo newToDo = new ToDo()
                                {
                                    ScheduleorAssignment = "Schedule",
                                    PeriodName = item.PeriodName,
                                    StageName = item.StageName,
                                    SubStageName = item.SubStageName,
                                    Date = item.Date,
                                    StartTime = item.StartTime,
                                    EndTime = item.EndTime
                                };
                                viewModel.ToDoList.Add(newToDo);
                            }
                            else if (item.Date == DateTime.Now)
                            {
                                if (item.StartTime > DateTime.Now.TimeOfDay)
                                {
                                    ToDo newToDo = new ToDo()
                                    {
                                        ScheduleorAssignment = "Schedule",
                                        PeriodName = item.PeriodName,
                                        StageName = item.StageName,
                                        SubStageName = item.SubStageName,
                                        Date = item.Date,
                                        StartTime = item.StartTime,
                                        EndTime = item.EndTime
                                    };
                                    viewModel.ToDoList.Add(newToDo);
                                }
                            }
                        }


                        foreach (var item in viewModel.CandidateScheduleList)
                        {
                            if (item.IDStage == 2 && item.Date.Date.CompareTo(DateTime.Now.Date) == 0 &&
                                DateTime.Now.TimeOfDay.CompareTo(item.StartTime) >= 0 &&
                                DateTime.Now.TimeOfDay.CompareTo(item.EndTime) <= 0)
                            {
                                viewModel.LogicTestSchedule = item;
                                break;
                            }
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CronJob 1 is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
