using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendFF.Models.DTO
{
    public class CreateAccountDTO
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int OrganizationID { get; set; }
        public string? AccountType { get; set; }
        public bool IsDarkMode { get; set; }
        public string? Password { get; set; }
    }
}