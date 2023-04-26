using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendFF.Models;
using backendFF.Services.Context;

namespace backendFF.Services
{
    public class UpdateLogService
    {
        private readonly DataContext _context;
        public UpdateLogService(DataContext context)
        {
            _context = context;
        }

        public bool AddUpdate(UpdateLogModel newUpdate)
        {
            _context.Add(newUpdate);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<UpdateLogModel> GetAllUpdates()
        {
            return _context.UpdateLog;
        }

        public IEnumerable<UpdateLogModel> GetUpdatesByYardID(int yardID)
        {
            return _context.UpdateLog.Where(update => update.YardID == yardID);
        }

        public UpdateLogModel GetLastYardUpdate(int yardID)
        {
            return _context.UpdateLog.Last(update => update.YardID == yardID);
        }

        public IEnumerable<UpdateLogModel> GetUpdatesByOrganizationID(int organizationID)
        {
            return _context.UpdateLog.Where(update => update.OrganizationID == organizationID);
        }

        public IEnumerable<UpdateLogModel> GetUpdatesByUserID(int userID)
        {
            return _context.UpdateLog.Where(update => update.UserID == userID);
        }
    }
}