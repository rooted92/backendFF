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
        public DateTime DateUpdated { get; set; }
        public int UpdateDriverID { get; set; }

        public UpdateLogModel() { }
    }
}