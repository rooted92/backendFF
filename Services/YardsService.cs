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

        public bool AddYard(YardModel newYard)
        {
            _context.Add(newYard);

            return _context.SaveChanges() != 0;
        }

        public IEnumerable<YardModel> GetAllYards()
        {
            return _context.YardInfo;
        }

        public IEnumerable<YardModel> GetAllYardsByOrganizationID(int organizationID)
        {
            return _context.YardInfo.Where(yard => yard.OrganizationID == organizationID);
        }

        public bool UpdateYard(YardModel yardToUpdate)
        {
            _context.Update<YardModel>(yardToUpdate);
            return _context.SaveChanges() != 0;
        }

        public bool DeleteYard(YardModel yardToDelete)
        {
            yardToDelete.IsDeleted = true;
            _context.Update<YardModel>(yardToDelete);
            return _context.SaveChanges() != 0;
        }
    }
}