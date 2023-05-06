using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendFF.Models;
using backendFF.Models.DTO;
using backendFF.Services;
using Microsoft.AspNetCore.Mvc;

namespace backendFF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrailerController : ControllerBase
    {
        private readonly TrailerService _data;
        public TrailerController(TrailerService dataFromService)
        {
            _data = dataFromService;
        }

        [HttpPost]
        [Route("AddTrailer/{driverID}")]
        public bool AddTrailer([FromBody]CreateTrailerDTO trailerToAdd, [FromRoute]int driverID)
        {
            return _data.AddTrailer(trailerToAdd, driverID);
        }

        [HttpGet]
        [Route("GetAllTrailers")]
        public IEnumerable<TrailerModel> GetAllTrailers()
        {
            return _data.GetAllTrailers();
        }

        [HttpGet]
        [Route("GetTrailersByPossessionID/{possessionID}")]
        public IEnumerable<TrailerModel> GetTrailersByPossessionID(int possessionID)
        {
            return _data.GetTrailersByPossessionID(possessionID);
        }

        [HttpGet]
        [Route("GetTrailersByOrganizationID/{organizationID}")]
        public IEnumerable<TrailerModel> GetTrailersByOrganizationID(int organizationID)
        {
            return _data.GetTrailersByOrganizationID(organizationID);
        }

        [HttpGet]
        [Route("GetTrailersInTransitByOrganizationID/{organizationID}")]
        public IEnumerable<TrailerModel> GetTrailersInTransitByOrganizationID(int organizationID)
        {
            return _data.GetTrailersInTransitByOrganizationID(organizationID);
        }

        [HttpPost]
        [Route("UpdateTrailer")]
        public bool UpdateTrailer(TrailerModel trailerToUpdate)
        {
            return _data.UpdateTrailer(trailerToUpdate);
        }

        [HttpPost]
        [Route("UpdateTrailerToInTransit/{driverID}")]
        public bool UpdateTrailerToInTransit([FromBody]TrailerModel trailerToUpdate, [FromRoute]int driverID)
        {
            return _data.UpdateTrailerToInTransit(trailerToUpdate, driverID);
        }

        [HttpPost]
        [Route("UpdateTrailerToYard/{yardID}")]
        public bool UpdateTrailerToYard([FromBody]TrailerModel trailerToUpdate, [FromRoute]int yardID)
        {
            return _data.UpdateTrailerToYard(trailerToUpdate, yardID);
        }

        [HttpPost]
        [Route("DeleteTrailer/{userID}")]
        public bool DeleteTrailer([FromBody]TrailerModel trailerToDelete, [FromRoute]int userID)
        {
            return _data.DeleteTrailer(trailerToDelete, userID);
        }

    }
}