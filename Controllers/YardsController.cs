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
    public class YardsController : ControllerBase
    {
        private readonly YardsService _data;
        public YardsController(YardsService dataFromService)
        {
            _data = dataFromService;
        }

        [HttpPost]
        [Route("AddYard/{userID}")]
        public bool AddYard(YardModel newYard, int userID)
        {
            return _data.AddYard(newYard, userID);
        }

        [HttpGet]
        [Route("GetAllYards")]
        public IEnumerable<YardModel> GetAllYards()
        {
            return _data.GetAllYards();
        }

        [HttpGet]
        [Route("GetAllYardsByOrganizationID/{organizationID}")]
        public IEnumerable<YardModel> GetAllYardsByOrganizationID(int organizationID)
        {
            return _data.GetAllYardsByOrganizationID(organizationID);
        }

        [HttpPost]
        [Route("UpdateYard/{userID}")]
        public bool UpdateYard(YardModel yardToUpdate, int userID)
        {
            return _data.UpdateYard(yardToUpdate, userID);
        }

        [HttpPost]
        [Route("DeleteYard/{userID}")]
        public bool DeleteYard(YardModel yardToDelete, int userID)
        {
            return _data.DeleteYard(yardToDelete, userID);
        }
    }
}