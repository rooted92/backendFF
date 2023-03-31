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
        [Route("AddYard")]
        public bool AddYard(YardModel newYard)
        {
            return _data.AddYard(newYard);
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
        [Route("UpdateYard")]
        public bool UpdateYard(YardModel yardToUpdate)
        {
            return _data.UpdateYard(yardToUpdate);
        }

        [HttpPost]
        [Route("DeleteYard")]
        public bool DeleteYard(YardModel yardToDelete)
        {
            return _data.DeleteYard(yardToDelete);
        }
    }
}