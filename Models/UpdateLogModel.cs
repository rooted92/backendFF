using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendFF.Models
{
    public class UpdateLogModel
    {
        public int ID { get; set; }
        public int YardID { get; set; }
        public int UserID { get; set; }
        public int OrganizationID { get; set; }
        public long DateUpdated { get; set; }
        public string? Details { get; set; }

        public UpdateLogModel() { }
    }
}