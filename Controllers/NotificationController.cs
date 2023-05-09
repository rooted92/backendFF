using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendFF.Models;
using backendFF.Services;
using Microsoft.AspNetCore.Mvc;

namespace backendFF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _data;
        public NotificationController(NotificationService dataFromService)
        {
            _data = dataFromService;
        }

        [HttpPost]
        [Route("AddNotification")]
        public bool AddNotification(NotificationModel newNotification)
        {
            return _data.AddNotification(newNotification);
        }

        [HttpGet]
        [Route("GetNotificationsByRecipientID/{recipientID}")]
        public IEnumerable<NotificationModel> GetNotificationsByRecipientID(int recipientID)
        {
            return _data.GetNotificationsByRecipientID(recipientID);
        }

        [HttpGet]
        [Route("GetAllOrganizationNotifications/{organizationID}")]
        public IEnumerable<NotificationModel> GetAllOrganizationNotifications(int organizationID)
        {
            return _data.GetAllOrganizationNotifications(organizationID);
        }
    }
}