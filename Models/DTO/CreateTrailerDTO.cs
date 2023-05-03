using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendFF.Models.DTO
{
    public class CreateTrailerDTO
    {
        public string? TrailerNumber { get; set; }
        public string? Type { get; set; }
        public string? Load { get; set; }
        public string? Cleanliness { get; set; }
        public string? FuelLevel { get; set; }
        public string? Length { get; set; }
        public string? Details { get; set; }
        public int PossessionID { get; set; }
        public int OrganizationID { get; set; }
    }
}