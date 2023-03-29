using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendFF.Models
{
    public class YardModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zipcode { get; set; }
        public int OrganizationID { get; set; }
        public bool IsDeleted { get; set; }

        public YardModel() { }
    }
}