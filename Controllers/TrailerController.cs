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
    public class TrailerController : ControllerBase
    {
        private readonly TrailerService _data;
        public TrailerController(TrailerService dataFromService)
        {
            _data = dataFromService;
        }

        [HttpPost]
        [Route("AddTrailer")]
        public bool AddTrailer(TrailerModel newTrailer)
        {
            return _data.AddTrailer(newTrailer);
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
        public bool UpdateTrailerToInTransit(TrailerModel trailerToUpdate, int driverID)
        {
            return _data.UpdateTrailerToInTransit(trailerToUpdate, driverID);
        }

        [HttpPost]
        [Route("DeleteTrailer")]
        public bool DeleteTrailer(TrailerModel trailerToDelete)
        {
            return _data.DeleteTrailer(trailerToDelete);
        }

    }
}