using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendFF.Models.DTO
{
    public class UpdateLogDTO
    {
        public int YardID { get; set; }
        public int DriverID { get; set; }
        public long DateUpdatedUnix { get; set; }
    }
}