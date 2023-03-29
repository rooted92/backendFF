using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendFF.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Salt { get; set; }
        public string? Hash { get; set; }
        public int OrganizationID { get; set; }
        public string? AccountType { get; set; }
        public bool IsDarkMode { get; set; }
        public bool IsDeleted { get; set; }

        public UserModel() { }
    }
}