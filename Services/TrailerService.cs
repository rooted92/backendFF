using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendFF.Models;
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

        public bool AddTrailer(TrailerModel newTrailer)
        {
            _context.TrailerInfo.Add(newTrailer);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<TrailerModel> GetAllTrailers()
        {
            return _context.TrailerInfo;
        }

        public IEnumerable<TrailerModel> GetTrailersByPossessionID(int possessionID)
        {
            return _context.TrailerInfo.Where(trailer => trailer.PossessionID == possessionID);
        }

        public IEnumerable<TrailerModel> GetTrailersByOrganizationID(int organizationID)
        {
            return _context.TrailerInfo.Where(trailer => trailer.OrganizationID == organizationID);
        }

        public IEnumerable<TrailerModel> GetTrailersInTransitByOrganizationID(int organizationID)
        {
            return _context.TrailerInfo.Where(trailer => trailer.OrganizationID == organizationID && trailer.InTransit == true);
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
            newUpdate.YardID = yardID;
            newUpdate.DriverID = driverID;
            newUpdate.DateUpdated = DateTime.Now;

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
            newUpdate.YardID = yardID;
            newUpdate.DriverID = driverID;
            newUpdate.DateUpdated = DateTime.Now;

            _context.UpdateLog.Add(newUpdate);
            if(_context.SaveChanges() != 0)
            {
                result = UpdateTrailer(trailerToUpdate);
            }
            
            return result;
        }

        public bool DeleteTrailer(TrailerModel trailerToDelete)
        {
            trailerToDelete.IsDeleted = true;
            return UpdateTrailer(trailerToDelete);
        }
    }
}