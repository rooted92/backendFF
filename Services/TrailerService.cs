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
            _context.Add(newTrailer);
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
            trailerToUpdate.PossessionID = driverID;
            trailerToUpdate.InTransit = true;
            return UpdateTrailer(trailerToUpdate);
        }



        public bool DeleteTrailer(TrailerModel trailerToDelete)
        {
            trailerToDelete.IsDeleted = true;
            return UpdateTrailer(trailerToDelete);
        }
    }
}