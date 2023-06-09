using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendFF.Models;
using backendFF.Models.DTO;
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
            return _context.OrganizationInfo.SingleOrDefault(organization => organization.Name == name && !organization.IsDeleted) != null;
        }

        public bool DoesOrganizationJoinCodeExist(string joinCode)
        {
            return _context.OrganizationInfo.SingleOrDefault(organization => organization.JoinCode == joinCode) != null;
        }

        public OrganizationModel? AddOrganization(CreateOrganizationDTO newOrganization)
        {
            OrganizationModel createdOrganization = new OrganizationModel();

            if(!DoesOrganizationExist(newOrganization.Name))
            {
                bool codeExists = true;
                while (codeExists)
                {
                    Random r = new Random();
                    int x = r.Next(0, 1000000);
                    createdOrganization.JoinCode = x.ToString("000000");
                    codeExists = DoesOrganizationJoinCodeExist(createdOrganization.JoinCode);
                }
                createdOrganization.ID = 0;
                createdOrganization.Name = newOrganization.Name;
                createdOrganization.IsDeleted = false;
                
                _context.Add(createdOrganization);

                if(_context.SaveChanges() != 0)
                {
                    return createdOrganization;
                }
            }

            return null;
        }

        public IEnumerable<OrganizationModel> GetAllOrganizations()
        {
            return _context.OrganizationInfo;
        }

        public OrganizationModel GetOrganizationByID(int ID)
        {
            return _context.OrganizationInfo.SingleOrDefault(organization => organization.ID == ID && !organization.IsDeleted);
        }

        public OrganizationModel GetOrganizationByMemberUserID(int memberUserID)
        {
            UserModel memberUser = _context.UserInfo.SingleOrDefault(user => user.ID == memberUserID);

            return GetOrganizationByID(memberUser.OrganizationID);
        }

        public OrganizationModel GetOrganizationByJoinCode(string joinCode)
        {
            return _context.OrganizationInfo.SingleOrDefault(organization => organization.JoinCode == joinCode && !organization.IsDeleted);
        }

        public bool UpdateOrganization(OrganizationModel organizationToUpdate)
        {
            _context.Update<OrganizationModel>(organizationToUpdate);
            
            return _context.SaveChanges() != 0;
        }
 
        public bool DeleteOrganization(OrganizationModel organizationToDelete)
        {
            organizationToDelete.IsDeleted = true;
            return UpdateOrganization(organizationToDelete);
        }
    }
}