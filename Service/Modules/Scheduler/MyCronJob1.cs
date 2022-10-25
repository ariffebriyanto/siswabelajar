using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Model.Subdomains.DashboardSubdomain.Candidate;

using Service.Modules.DashboardModule;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Service.Modules.LoginModule;
using OneStopRecruitment.Areas.DashboardArea.ViewModels.Candidate;
using System.Linq;

//---------------------------------------//
//Nama author: arif
//EMail: arifpebri0202@yahoo.co.id

//---------------------------------------//

namespace Service.Modules
{
    public class MyCronJob1 : CronJobService
    {
        private readonly ICandidateService candidateService;
        private readonly ILogger<MyCronJob1> _logger;
        private readonly IMainService MainService;

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



        DashboardCandidateViewModel viewModel = new DashboardCandidateViewModel();

        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("{now} CronJob 1 is working.", DateTime.Now.ToString("T"));

          
            List<Model.Subdomains.EmailSubdomain.User> Logins = MainService.GetActiveUser();



            if (Logins != null)
            {

                foreach (var Login in Logins)
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
                            // viewModel.ToDoList.Add(newToDo);
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


                    
                    DateTime hariini = DateTime.Now.Date;

                    if (viewModel.ToDoList.Count > 0)
                    {


                        foreach (var item in viewModel.ToDoList)
                        {
                            var item1 = viewModel.ToDoList;

                               

                            if (item.ScheduleorAssignment == "Schedule") /// to scheduler scedhule list
                            {
                                var result = item1.Where(y => y.StageName == item.StageName && y.SubStageName == item.SubStageName);
                                
                                    if (result != null)
                                {
                                    DateTime? hariterkahir = result.Max(x => x.Date);
                                    if (hariterkahir.HasValue)
                                    {
                                        hariterkahir = hariterkahir.Value.Date;
                                        DateTime satuhari = hariterkahir.Value.Date.AddDays(-1);
                                    }
                                }
                                    

                                DateTime tujuhago = item.Date.AddDays(-7);
                                if (tujuhago > hariini && tujuhago == hariini)
                                {
                                    if (hariini.TimeOfDay.Hours == 0 && hariini.TimeOfDay.Minutes == 1)
                                    {
                                        //todo scheduler


                                    }
                                }

                            }
                            else if (item.ScheduleorAssignment == "Assignment") // to do schedule assigment list
                            {

                                if (item.DeadlineStartDate == hariini)
                                {
                                    if (hariini.TimeOfDay.Hours == 0 && hariini.TimeOfDay.Minutes == 1)
                                    {
                                        //todo scheduler


                                    }


                                }

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
