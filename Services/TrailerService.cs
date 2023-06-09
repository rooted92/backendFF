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

        public bool AddTrailer(List<CreateTrailerDTO> trailersToAdd, int driverID)
        {
            bool result = false;

            List<TrailerModel> foundTrailers = GetTrailersByYardID(trailersToAdd[0].PossessionID).ToList();
            for (int j = 0; j < foundTrailers.Count; j++)
            {
                _context.TrailerInfo.Remove(foundTrailers[j]);
            }

            for (int i = 0; i < trailersToAdd.Count; i++)
            {
                TrailerModel newTrailer = new TrailerModel();
                newTrailer.ID = 0;
                newTrailer.TrailerNumber = trailersToAdd[i].TrailerNumber;
                newTrailer.Type = trailersToAdd[i].Type;
                newTrailer.Load = trailersToAdd[i].Load;
                newTrailer.Cleanliness = trailersToAdd[i].Cleanliness;
                newTrailer.FuelLevel = trailersToAdd[i].FuelLevel;
                newTrailer.Length = trailersToAdd[i].Length;
                newTrailer.Details = trailersToAdd[i].Details;
                newTrailer.PossessionID = trailersToAdd[i].PossessionID;
                newTrailer.OrganizationID = trailersToAdd[i].OrganizationID;
                newTrailer.InTransit = false;
                newTrailer.IsDeleted = false;

                _context.TrailerInfo.Add(newTrailer);

                if (_context.SaveChanges() != 0)
                {
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

                    result = _context.SaveChanges() != 0;
                }
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

        public IEnumerable<TrailerModel> GetTrailersByYardID(int yardID)
        {
            return _context.TrailerInfo.Where(trailer => trailer.PossessionID == yardID && trailer.InTransit == false && !trailer.IsDeleted);
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
            _context.TrailerInfo.Update(trailerToUpdate);
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
            if (_context.SaveChanges() != 0)
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
            if (_context.SaveChanges() != 0)
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
            if (_context.SaveChanges() != 0)
            {
                result = UpdateTrailer(trailerToDelete);
            }

            return result;
        }
    }
}