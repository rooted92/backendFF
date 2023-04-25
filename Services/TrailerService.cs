using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendFF.Models;
using backendFF.Models.DTO;
using backendFF.Services.Context;

namespace backendFF.Services
{
    public class TrailerService
    {
        private readonly DataContext _context;
        public TrailerService(DataContext context)
        {
            _context = context;
        }

        public bool AddTrailer(CreateTrailerDTO trailerToAdd, int driverID)
        {
            bool result = false;

            TrailerModel newTrailer = new TrailerModel();
            newTrailer.ID = 0;
            newTrailer.TrailerNumber = trailerToAdd.TrailerNumber;
            newTrailer.Type = trailerToAdd.Type;
            newTrailer.Load = trailerToAdd.Load;
            newTrailer.Cleanliness = trailerToAdd.Cleanliness;
            newTrailer.FuelLevel = trailerToAdd.FuelLevel;
            newTrailer.Length = trailerToAdd.Length;
            newTrailer.Details = trailerToAdd.Details;
            newTrailer.PossessionID = trailerToAdd.PossessionID;
            newTrailer.OrganizationID = trailerToAdd.OrganizationID;
            newTrailer.InTransit = false;
            newTrailer.IsDeleted = false;

            UpdateLogModel newUpdate = new UpdateLogModel();
            newUpdate.ID = 0;
            newUpdate.YardID = newTrailer.PossessionID;
            newUpdate.UserID = driverID;
            newUpdate.OrganizationID = newTrailer.OrganizationID;
            DateTime currentTime = DateTime.UtcNow;
            newUpdate.DateUpdated = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
            string yardName = _context.YardInfo.SingleOrDefault(yard => yard.ID == newTrailer.PossessionID).Name;
            newUpdate.Details = $"Trailer #{newTrailer.TrailerNumber} Added to {yardName}";

            _context.UpdateLog.Add(newUpdate);
            if(_context.SaveChanges() != 0)
            {
                _context.TrailerInfo.Add(newTrailer);
                return _context.SaveChanges() != 0;
            }
            
            return result;
        }

        public IEnumerable<TrailerModel> GetAllTrailers()
        {
            return _context.TrailerInfo;
        }

        public IEnumerable<TrailerModel> GetTrailersByPossessionID(int possessionID)
        {
            return _context.TrailerInfo.Where(trailer => trailer.PossessionID == possessionID && !trailer.IsDeleted);
        }

        public IEnumerable<TrailerModel> GetTrailersByOrganizationID(int organizationID)
        {
            return _context.TrailerInfo.Where(trailer => trailer.OrganizationID == organizationID && !trailer.IsDeleted);
        }

        public IEnumerable<TrailerModel> GetTrailersInTransitByOrganizationID(int organizationID)
        {
            return _context.TrailerInfo.Where(trailer => trailer.OrganizationID == organizationID && trailer.InTransit == true && !trailer.IsDeleted);
        }

        public bool UpdateTrailer(TrailerModel trailerToUpdate)
        {
            _context.Update<TrailerModel>(trailerToUpdate);
            return _context.SaveChanges() != 0;
        }

        public bool UpdateTrailerToInTransit(TrailerModel trailerToUpdate, int driverID)
        {
            bool result = false;

            int yardID = trailerToUpdate.PossessionID;
            trailerToUpdate.PossessionID = driverID;
            trailerToUpdate.InTransit = true;

            UpdateLogModel newUpdate = new UpdateLogModel();
            newUpdate.ID = 0;
            newUpdate.YardID = yardID;
            newUpdate.UserID = driverID;
            newUpdate.OrganizationID = trailerToUpdate.OrganizationID;
            DateTime currentTime = DateTime.UtcNow;
            newUpdate.DateUpdated = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
            string yardName = _context.YardInfo.SingleOrDefault(yard => yard.ID == yardID).Name;
            newUpdate.Details = $"Trailer #{trailerToUpdate.TrailerNumber} Changed To {driverID} From {yardName}";

            _context.UpdateLog.Add(newUpdate);
            if(_context.SaveChanges() != 0)
            {
                result = UpdateTrailer(trailerToUpdate);
            }
            
            return result;
        }

        public bool UpdateTrailerToYard(TrailerModel trailerToUpdate, int yardID)
        {
            bool result = false;

            int driverID = trailerToUpdate.PossessionID;
            trailerToUpdate.PossessionID = yardID;
            trailerToUpdate.InTransit = true;

            UpdateLogModel newUpdate = new UpdateLogModel();
            newUpdate.ID = 0;
            newUpdate.YardID = yardID;
            newUpdate.UserID = driverID;
            newUpdate.OrganizationID = trailerToUpdate.OrganizationID;
            DateTime currentTime = DateTime.UtcNow;
            newUpdate.DateUpdated = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
            string yardName = _context.YardInfo.SingleOrDefault(yard => yard.ID == yardID).Name;
            newUpdate.Details = $"Trailer #{trailerToUpdate.TrailerNumber} Added To {yardName} From {driverID}";

            _context.UpdateLog.Add(newUpdate);
            if(_context.SaveChanges() != 0)
            {
                result = UpdateTrailer(trailerToUpdate);
            }
            
            return result;
        }

        public bool DeleteTrailer(TrailerModel trailerToDelete, int userID)
        {
            trailerToDelete.IsDeleted = true;
            bool result = false;

            UpdateLogModel newUpdate = new UpdateLogModel();
            newUpdate.ID = 0;
            newUpdate.YardID = trailerToDelete.PossessionID;
            newUpdate.UserID = userID;
            newUpdate.OrganizationID = trailerToDelete.OrganizationID;
            DateTime currentTime = DateTime.UtcNow;
            newUpdate.DateUpdated = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
            newUpdate.Details = $"Trailer #{trailerToDelete.TrailerNumber} Deleted By {userID}";

            _context.UpdateLog.Add(newUpdate);
            if(_context.SaveChanges() != 0)
            {
                result = UpdateTrailer(trailerToDelete);
            }
            
            return result;
        }
    }
}