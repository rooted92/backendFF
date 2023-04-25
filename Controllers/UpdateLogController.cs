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
    public class UpdateLogController : ControllerBase
    {
        private readonly UpdateLogService _data;
        public UpdateLogController(UpdateLogService dataFromService)
        {
            _data = dataFromService;
        }

        [HttpPost]
        [Route("AddUpdate")]
        public bool AddUpdate(UpdateLogModel newUpdate)
        {
            return _data.AddUpdate(newUpdate);
        }

        [HttpGet]
        [Route("GetAllUpdates")]
        public IEnumerable<UpdateLogModel> GetAllUpdates()
        {
            return _data.GetAllUpdates();
        }

        [HttpGet]
        [Route("GetUpdatesByYardID/{yardID}")]
        public IEnumerable<UpdateLogModel> GetUpdatesByYardID(int yardID)
        {
            return _data.GetUpdatesByYardID(yardID);
        }

        [HttpGet]
        [Route("GetLastYardUpdate/{yardID}")]
        public UpdateLogModel GetLastYardUpdate(int yardID)
        {
            return _data.GetLastYardUpdate(yardID);
        }

        [HttpGet]
        [Route("GetUpdatesByUserID/{userID}")]
        public IEnumerable<UpdateLogModel> GetUpdatesByUserID(int userID)
        {
            return _data.GetUpdatesByUserID(userID);
        }
    }
}