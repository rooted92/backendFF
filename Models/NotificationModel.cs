using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendFF.Models
{
    public class NotificationModel
    {
        public int ID { get; set; }
        public string? Details { get; set; }
        public long TimeStamp { get; set; }
        public int RecipientID { get; set; }
        public bool ToDriver { get; set; }
        public bool IsRead { get; set; }
    }
}