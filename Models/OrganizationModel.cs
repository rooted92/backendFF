using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendFF.Models
{
    public class OrganizationModel
    {
        public int ID { get; set; }
        public int JoinCode { get; set; }
        public int OwnerUserID { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }

        public OrganizationModel() { }
    }
}