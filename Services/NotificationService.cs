using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendFF.Models;
using backendFF.Services.Context;

namespace backendFF.Services
{
    public class NotificationService
    {
        private readonly DataContext _context;
        public NotificationService(DataContext context)
        {
            _context = context;
        }

        public bool AddNotification(NotificationModel newNotification)
        {
            _context.Add(newNotification);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<NotificationModel> GetNotificationsByRecipientID(int recipientID)
        {
            return _context.NotificationInfo.Where(notification => notification.RecipientID == recipientID && notification.ToDriver && !notification.IsRead);
        }

        public IEnumerable<NotificationModel> GetAllOrganizationNotifications(int organizationID)
        {
            return _context.NotificationInfo.Where(notification => notification.RecipientID == organizationID && !notification.ToDriver && !notification.IsRead);
        }
    }
}