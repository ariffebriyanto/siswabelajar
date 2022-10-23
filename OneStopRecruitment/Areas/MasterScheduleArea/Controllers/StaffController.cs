using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.DBConstraint;
using Model.Subdomains.DropdownSubdomain;
using Model.Subdomains.MasterScheduleSubdomain;
using OneStopRecruitment.Areas.MasterScheduleArea.ViewModels.Staff;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.AuthenticationHelpers;
using OneStopRecruitment.Helpers.DropdownHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.RequestHelpers;
using OneStopRecruitment.Models;
using Service.Modules.MasterScheduleModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneStopRecruitment.Areas.MasterScheduleArea.Controllers
{
    [Area("MasterScheduleArea")]
    public class StaffController : BaseController
    {
        private readonly IStaffService scheduleService;

        public StaffController(IStaffService scheduleService)
        {
            this.scheduleService = scheduleService;                                                            
        }

        private IEnumerable<SelectListItem> GetPeriodDropdown()
        {
            List<DropdownItem> roleDropdownList = new List<DropdownItem>();
            var periodList = scheduleService.GetAllPeriod();
            foreach (var item in periodList)
            {
                DropdownItem roleDropdown = new DropdownItem();
                roleDropdown.Value = item.IDPeriod.ToString();
                roleDropdown.Text = item.PeriodName;
                roleDropdownList.Add(roleDropdown);
            }
            IEnumerable<SelectListItem> result = new SelectListItemBuilder()
                .AddRangeDropdownItems(roleDropdownList.AsEnumerable())
                .Build();
            return result;
        }

        private IEnumerable<SelectListItem> GetStageDropdown()
        {
            List<DropdownItem> roleDropdownList = new List<DropdownItem>();
            var stageList = scheduleService.GetStages();
            foreach (var item in stageList)
            {
                DropdownItem roleDropdown = new DropdownItem();
                roleDropdown.Value = item.IDStage.ToString();
                roleDropdown.Text = item.StageName;
                roleDropdownList.Add(roleDropdown);
            }
            IEnumerable<SelectListItem> result = new SelectListItemBuilder()
                .AddRangeDropdownItems(roleDropdownList.AsEnumerable())
                .Build();
            return result;
        }

        private IEnumerable<SelectListItem> GetPositionDropdown()
        {
            List<DropdownItem> roleDropdownList = new List<DropdownItem>();
            var positionList = scheduleService.GetPositions();
            foreach (var item in positionList)
            {
                DropdownItem roleDropdown = new DropdownItem();
                roleDropdown.Value = item.IDPosition;
                roleDropdown.Text = item.PositionName;
                roleDropdownList.Add(roleDropdown);
            }
            IEnumerable<SelectListItem> result = new SelectListItemBuilder()
                .AddRangeDropdownItems(roleDropdownList.AsEnumerable())
                .Build();
            return result;
        }

        [HttpGet]
        public IActionResult Index()
        {

            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MasterScheduleViewModel viewModel = new MasterScheduleViewModel();
            viewModel.ScheduleList = new List<MasterSchedule>();
            viewModel.PeriodList = GetPeriodDropdown().ToList();
            viewModel.StageList = GetStageDropdown().ToList();
            viewModel.PositionDropdown = GetPositionDropdown().ToList();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(MasterScheduleViewModel viewModel)
        {            
            viewModel.PeriodList = GetPeriodDropdown().ToList();
            viewModel.StageList = GetStageDropdown().ToList();
            viewModel.PositionDropdown = GetPositionDropdown().ToList();
            return View("Index", viewModel);
        }

        public List<SubStage> GetSubStageList(int IDStage)
        {
            return scheduleService.GetSubStageByStageID(IDStage);
        }

        private void ValidateInput(MasterSchedule schedule)
        {
            List<SubStage> subStageList = scheduleService.GetSubStageByStageID(schedule.IDStage);
            if (subStageList != null && subStageList.Count > 0 && schedule.IDSubStage == 0)
            {
                ModelState.AddModelError("MasterSchedule.IDSubStage", AlertConstraint.Schedule.EmptySubStage);
            }
        }

        [HttpGet]
        public IActionResult InsertSchedule()
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MasterScheduleViewModel viewModel = new MasterScheduleViewModel();
            viewModel.Schedule = new MasterSchedule();
            viewModel.Schedule.Date = DateTime.Now;
            viewModel.Schedule.StartTime = TimeSpan.Zero;
            viewModel.Schedule.EndTime = TimeSpan.Zero;

            viewModel.PeriodList = GetPeriodDropdown().ToList();
            viewModel.StageList = GetStageDropdown().ToList();
            viewModel.PositionList = scheduleService.GetPositions();
            return View("InsertUpdateSchedule", viewModel);
        }

        [EncryptedActionParameter]
        [HttpGet]
        public IActionResult UpdateSchedule(string ScheduleID)
        {
            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);
            MasterScheduleViewModel viewModel = new MasterScheduleViewModel();
            viewModel.Schedule = scheduleService.GetScheduleByID(new Guid(ScheduleID));
            viewModel.PeriodList = GetPeriodDropdown().ToList();
            viewModel.StageList = GetStageDropdown().ToList();
            viewModel.PositionList = scheduleService.GetPositions();
            return View("InsertUpdateSchedule", viewModel);
        }

        [HttpPost]
        public IActionResult InsertSchedule(MasterSchedule schedule)
        {
            MasterScheduleViewModel viewModel = new MasterScheduleViewModel()
            {
                Schedule = schedule,
                PeriodList = GetPeriodDropdown().ToList(),
                StageList = GetStageDropdown().ToList(),
                PositionList = scheduleService.GetPositions()
            };

            ValidateInput(schedule);

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateSchedule", viewModel);
            }

            try
            {
                bool result = scheduleService.InsertSchedule(schedule);
                if (result)
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdateSchedule", viewModel);
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateSchedule", viewModel);
            }            
        }

        [HttpPost]
        public IActionResult UpdateSchedule(MasterSchedule schedule)
        {
            MasterScheduleViewModel viewModel = new MasterScheduleViewModel()
            {
                Schedule = schedule,
                PeriodList = GetPeriodDropdown().ToList(),
                StageList = GetStageDropdown().ToList(),
                PositionList = scheduleService.GetPositions()
            };

            ValidateInput(schedule);

            if (!ModelState.IsValid)
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.RequiredForm));
                return View("InsertUpdateSchedule", viewModel);
            }

            try
            {
                bool result = scheduleService.UpdateSchedule(schedule);
                if (result)
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Success, AlertConstraint.Default.Success));
                    return RedirectToAction("Index");
                }
                else
                {
                    AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Failed));
                    return View("InsertUpdateSchedule", viewModel);
                }
            }
            catch
            {
                AddNotification(PopUpNotification.Notify(BaseConstraint.NotificationType.Failed, AlertConstraint.Default.Error));
                return View("InsertUpdateSchedule", viewModel);
            }
        }
    }
}