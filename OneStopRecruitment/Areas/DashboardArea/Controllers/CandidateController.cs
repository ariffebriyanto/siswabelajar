using Microsoft.AspNetCore.Mvc;
using Model.DBConstraint;
using Model.Subdomains.DashboardSubdomain.Candidate;
using OneStopRecruitment.Areas.DashboardArea.ViewModels.Candidate;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.RequestHelpers;
using Service.Modules.DashboardModule;
using System;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.DashboardArea.Controllers
{
    [Area("DashboardArea")]
    public class CandidateController : BaseController
    {
        private readonly ICandidateService candidateService;
        public CandidateController(ICandidateService candidateService)
        {            
            this.candidateService = candidateService;
        }

        public IActionResult Index()
        {
            DashboardCandidateViewModel viewModel = new DashboardCandidateViewModel();
            Login login = HttpContext.Session.GetLoggedinUser();

            Dashboard dashboard = new Dashboard();
            dashboard.CurrentPeriod = candidateService.GetCurrentPeriod(login.Username);
            dashboard.CurrentStage = candidateService.GetCurrentStage(login.Username);
            viewModel.Dashboard = dashboard;

            viewModel.ScheduleList = candidateService.GetAvailableSchedules(login.Username, login.IDUser);
            viewModel.CandidateScheduleList = candidateService.GetCandidateSchedules(login.IDUser);
            viewModel.AssignmentList = candidateService.GetAvailableAssignments(login.Username);
            viewModel.BlastEmailList = candidateService.GetNotifications();
            viewModel.ToDoList = new List<ToDo>();

            List<Assignment> unsubmittedAssignment = candidateService.GetUnsubmittedAssignments(login.Username, login.IDUser);
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
                        DeadlineStartDate = item.DeadlineStartDate,
                        DeadlineEndDate = item.DeadlineEndDate
                    };
                    viewModel.ToDoList.Add(newToDo);
                }
            }

            List<MasterSchedule> unregisteredSchedule = candidateService.GetUnenrolledSchedules(login.Username, login.IDUser);
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
                if(item.IDStage == 2 && item.Date.Date.CompareTo(DateTime.Now.Date) == 0 &&
                    DateTime.Now.TimeOfDay.CompareTo(item.StartTime) >=0 &&
                    DateTime.Now.TimeOfDay.CompareTo(item.EndTime) <= 0)
                {
                    viewModel.LogicTestSchedule = item;
                    break;
                }
            }

            return View(viewModel);
        }

        [EncryptedActionParameter]
        [HttpPost]
        public IActionResult CancelSchedule(string IDSchedule)
        {            
            try
            {
                MasterSchedule schedule = candidateService.GetScheduleByID(new Guid(IDSchedule));
                if ((schedule.Date.Date.CompareTo(DateTime.Now.Date) == 0 &&
                                          schedule.StartTime.CompareTo(DateTime.Now.TimeOfDay) > 0) ||
                                          schedule.Date.Date.CompareTo(DateTime.Now.Date) > 0)
                {
                    Login login = HttpContext.Session.GetLoggedinUser();
                    bool result = candidateService.CancelCandidateSchedule(new Guid(IDSchedule), login.IDUser);
                    if (result)
                    {
                        return Json(new { status = BaseConstraint.NotificationType.Success, message = AlertConstraint.Default.Success });
                    }
                    else
                    {
                        return Json(new { status = BaseConstraint.NotificationType.Failed, message = AlertConstraint.Default.Failed });
                    }
                }
                else
                {
                    // Already Past
                    return Json(new { status = BaseConstraint.NotificationType.Failed, message = AlertConstraint.Schedule.ScheduleAlreadyStarted });
                }
            }
            catch
            {
                return Json(new { status = BaseConstraint.NotificationType.Failed, message = AlertConstraint.Default.Error });
            }
        }

        [EncryptedActionParameter]
        [HttpPost]
        public IActionResult EnrollSchedule(string IDSchedule)
        {
            try
            {
                MasterSchedule schedule = candidateService.GetScheduleByID(new Guid(IDSchedule));
                if ((schedule.Date.Date.CompareTo(DateTime.Now.Date) == 0 &&
                                          schedule.StartTime.CompareTo(DateTime.Now.TimeOfDay) > 0) ||
                                          schedule.Date.Date.CompareTo(DateTime.Now.Date) > 0)
                {
                    Login login = HttpContext.Session.GetLoggedinUser();
                    bool result = candidateService.EnrollSchedule(new Guid(IDSchedule), login.IDUser);
                    if (result)
                    {
                        return Json(new { status = BaseConstraint.NotificationType.Success, message = AlertConstraint.Default.Success });
                    }
                    else
                    {
                        return Json(new { status = BaseConstraint.NotificationType.Failed, message = AlertConstraint.Default.Failed });
                    }
                }
                else
                {
                    // Already Past
                    return Json(new { status = BaseConstraint.NotificationType.Failed, message = AlertConstraint.Schedule.ScheduleAlreadyStarted });
                }
            }
            catch
            {
                return Json(new { status = BaseConstraint.NotificationType.Failed, message = AlertConstraint.Default.Error });
            }
        }
    }
}
