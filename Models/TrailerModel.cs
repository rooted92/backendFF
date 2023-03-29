using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendFF.Models
{
    public class TrailerModel
    {
        public int ID { get; set; }
        public string? TrailerNumber { get; set; }
        public string? Type { get; set; }
        public string? Load { get; set; }
        public string? Cleanliness { get; set; }
        public string? FuelLevel { get; set; }
        public string? Length { get; set; }
        public string? Details { get; set; }
        public int PossessionID { get; set; }
        public int OrganizationID { get; set; }
        public bool InTransit { get; set; }
        public bool IsDeleted { get; set; }

        public TrailerModel() { }
    }
}