using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendFF.Models;
using backendFF.Services.Context;

namespace backendFF.Services
{
    public class YardsService
    {
        private readonly DataContext _context;
        public YardsService(DataContext context)
        {
            _context = context;
        }

        public bool AddYard(YardModel newYard, int userID)
        {
            bool result = false;

            UpdateLogModel newUpdate = new UpdateLogModel();
            newUpdate.ID = 0;
            newUpdate.YardID = newYard.ID;
            newUpdate.UserID = userID;
            newUpdate.OrganizationID = newYard.OrganizationID;
            DateTime currentTime = DateTime.UtcNow;
            newUpdate.DateUpdated = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
            newUpdate.Details = $"{newYard.Name} Created";

            _context.UpdateLog.Add(newUpdate);
            if(_context.SaveChanges() != 0)
            {
                _context.Add(newYard);
                return _context.SaveChanges() != 0;
            }

            return result;
        }

        public IEnumerable<YardModel> GetAllYards()
        {
            return _context.YardInfo;
        }

        public IEnumerable<YardModel> GetAllYardsByOrganizationID(int organizationID)
        {
            return _context.YardInfo.Where(yard => yard.OrganizationID == organizationID && !yard.IsDeleted);
        }

        public bool UpdateYard(YardModel yardToUpdate, int userID)
        {
            bool result = false;

            UpdateLogModel newUpdate = new UpdateLogModel();
            newUpdate.ID = 0;
            newUpdate.YardID = yardToUpdate.ID;
            newUpdate.UserID = userID;
            newUpdate.OrganizationID = yardToUpdate.OrganizationID;
            DateTime currentTime = DateTime.UtcNow;
            newUpdate.DateUpdated = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
            newUpdate.Details = $"{yardToUpdate.Name} Updated";

            _context.UpdateLog.Add(newUpdate);
            if(_context.SaveChanges() != 0)
            {
                _context.Update<YardModel>(yardToUpdate);
                return _context.SaveChanges() != 0;
            }

            return result;
        }

        public bool DeleteYard(YardModel yardToDelete, int userID)
        {
            yardToDelete.IsDeleted = true;
            bool result = false;

            UpdateLogModel newUpdate = new UpdateLogModel();
            newUpdate.ID = 0;
            newUpdate.YardID = yardToDelete.ID;
            newUpdate.UserID = userID;
            newUpdate.OrganizationID = yardToDelete.OrganizationID;
            DateTime currentTime = DateTime.UtcNow;
            newUpdate.DateUpdated = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
            newUpdate.Details = $"{yardToDelete.Name} Deleted";

            _context.UpdateLog.Add(newUpdate);
            if(_context.SaveChanges() != 0)
            {
                _context.Update<YardModel>(yardToDelete);
                return _context.SaveChanges() != 0;
            }

            return result;
        }
    }
}