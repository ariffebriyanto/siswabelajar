using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneStopRecruitment.Models;
using System.Collections.Generic;

namespace OneStopRecruitment.Controllers
{
    public class BaseController : Controller
    {
        private readonly List<PopUpNotification> notificationList = new List<PopUpNotification>();
        public void AddNotification(PopUpNotification popUpNotification)
        {
            notificationList.Add(popUpNotification);
            TempData["ViewNotifications"] = JsonConvert.SerializeObject(notificationList);
        }
    }
}
