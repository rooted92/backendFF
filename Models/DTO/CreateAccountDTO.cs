using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendFF.Models.DTO
{
    public class CreateAccountDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OrganizationJoinCode { get; set; }
        public string? AccountType { get; set; }
        public string? Password { get; set; }
    }
}