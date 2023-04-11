using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendFF.Models;
using backendFF.Services;
using Microsoft.AspNetCore.Mvc;

namespace backendFF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationService _data;
        public OrganizationController(OrganizationService dataFromService)
        {
            _data = dataFromService;
        }

        [HttpPost]
        [Route("AddOrganization")]
        public bool AddOrganization(OrganizationModel newOrganization)
        {
            return _data.AddOrganization(newOrganization);
        }

        [HttpGet]
        [Route("GetAllOrganizations")]
        public IEnumerable<OrganizationModel> GetAllOrganizations()
        {
            return _data.GetAllOrganizations();
        }

        [HttpGet]
        [Route("GetOrganizationByID/{organizationID}")]
        public IEnumerable<OrganizationModel> GetOrganizationByID(int organizationID)
        {
            return _data.GetOrganizationByID(organizationID);
        }

        [HttpGet]
        [Route("GetOrganizationByMemberUserID/{memberUserID}")]
        public IEnumerable<OrganizationModel> GetOrganizationByMemberUserID(int memberUserID)
        {
            return _data.GetOrganizationByMemberUserID(memberUserID);
        }

        [HttpGet]
        [Route("GetOrganizationByJoinCode/{joinCode}")]
        public IEnumerable<OrganizationModel> GetOrganizationByJoinCode(string joinCode)
        {
            return _data.GetOrganizationByJoinCode(joinCode);
        }

        [HttpPost]
        [Route("UpdateOrganization")]
        public bool UpdateOrganization(OrganizationModel organizationToUpdate)
        {
            return _data.UpdateOrganization(organizationToUpdate);
        }

        [HttpPost]
        [Route("DeleteOrganization")]
        public bool DeleteOrganization(OrganizationModel organizationToDelete)
        {
            return _data.DeleteOrganization(organizationToDelete);
        }
    }
}