using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendFF.Models;
using backendFF.Services.Context;

namespace backendFF.Services
{
    public class OrganizationService
    {
         private readonly DataContext _context; 
        public OrganizationService(DataContext context)
        {
            _context = context;
        }

        public bool DoesOrganizationExist(string? name)
        {
            return _context.OrganizationInfo.SingleOrDefault(organization => organization.Name == name) != null;
        }

        public bool DoesOrganizationJoinCodeExist(string joinCode)
        {
            return _context.OrganizationInfo.SingleOrDefault(organization => organization.JoinCode == joinCode) != null;
        }

         public bool AddOrganization(OrganizationModel newOrganization)
        {
            bool result = false;

            if(!DoesOrganizationExist(newOrganization.Name))
            {
                bool codeExists = true;
                while (codeExists)
                {
                    Random r = new Random();
                    int x = r.Next(0, 1000000);
                    newOrganization.JoinCode = x.ToString("000000");
                    codeExists = DoesOrganizationJoinCodeExist(newOrganization.JoinCode);
                }
                
                _context.Add(newOrganization);

                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public IEnumerable<OrganizationModel> GetAllOrganizations()
        {
            return _context.OrganizationInfo;
        }

        public IEnumerable<OrganizationModel> GetOrganizationByID(int ID)
        {
            return _context.OrganizationInfo.Where(organization => organization.ID == ID);
        }

        public IEnumerable<OrganizationModel> GetOrganizationByOwnerUserID(int ownerUserID)
        {
            return _context.OrganizationInfo.Where(organization => organization.OwnerUserID == ownerUserID);
        }

        public IEnumerable<OrganizationModel> GetOrganizationByMemberUserID(int memberUserID)
        {
            UserModel memberUser = _context.UserInfo.SingleOrDefault(user => user.ID == memberUserID);

            return GetOrganizationByID(memberUser.OrganizationID);
        }

        public IEnumerable<OrganizationModel> GetOrganizationByJoinCode(string joinCode)
        {
            return _context.OrganizationInfo.Where(organization => organization.JoinCode == joinCode);
        }

        public bool UpdateOrganization(OrganizationModel organizationToUpdate)
        {
            _context.Update<OrganizationModel>(organizationToUpdate);
            
            return _context.SaveChanges() != 0;
        }

        public bool DeleteOrganization(OrganizationModel organizationToDelete)
        {
            organizationToDelete.IsDeleted = true;
            _context.Update<OrganizationModel>(organizationToDelete);
            return _context.SaveChanges() != 0;
        }
    }
}