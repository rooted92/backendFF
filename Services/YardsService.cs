using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendFF.Models;
using backendFF.Models.DTO;
using backendFF.Services.Context;

namespace backendFF.Services
{
    public class YardsService
    {
        private readonly DataContext _context;
        private readonly UpdateLogService _service;
        public YardsService(DataContext context, UpdateLogService service)
        {
            _context = context;
            _service = service;
        }

        public bool AddYard(CreateYardDTO newYard, int userID)
        {
            YardModel yardToAdd = new YardModel();
            yardToAdd.ID = 0;
            yardToAdd.Name = newYard.Name;
            yardToAdd.Address = newYard.Address;
            yardToAdd.City = newYard.City;
            yardToAdd.State = newYard.State;
            yardToAdd.Zipcode = newYard.Zipcode;
            yardToAdd.OrganizationID = newYard.OrganizationID;
            yardToAdd.IsDeleted = false;

            _context.Add(yardToAdd);
            _context.SaveChanges();

                UpdateLogModel newUpdate = new UpdateLogModel();
                newUpdate.ID = 0;
                newUpdate.YardID = _context.YardInfo.Count();
                newUpdate.UserID = userID;
                newUpdate.OrganizationID = newYard.OrganizationID;
                DateTime currentTime = DateTime.UtcNow;
                newUpdate.DateUpdated = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
                newUpdate.Details = $"{newYard.Name} Created";
                return _service.AddUpdate(newUpdate);
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
            if (_context.SaveChanges() != 0)
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
            if (_context.SaveChanges() != 0)
            {
                _context.Update<YardModel>(yardToDelete);
                return _context.SaveChanges() != 0;
            }

            return result;
        }
    }
}