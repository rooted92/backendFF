using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendFF.Models;
using backendFF.Models.DTO;
using backendFF.Services;
using Microsoft.AspNetCore.Mvc;

namespace backendFF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _data;
        public UserController(UserService dataFromService)
        {
            _data = dataFromService;
        }

        // Add user
        [HttpPost]
        [Route("AddUser")]
        public bool AddUser(CreateAccountDTO userToAdd)
        {
            return _data.AddUser(userToAdd);
        }

        // Login
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginDTO user)
        {
            return _data.Login(user);
        }

        // Update user
        [HttpPost]
        [Route("UpdateUser")]
        public bool UpdateUser(UserModel userToUpdate)
        {
            return _data.UpdateUser(userToUpdate);
        }

        // Update user password
        [HttpPost]
        [Route("UpdateUser/{id}/{password}")]
        public bool UpdateUser(int id, string password)
        {
            return _data.UpdatePassword(id, password);
        }

        // Update user dark mode setting
        [HttpPost]
        [Route("UpdateUser/{id}/{isDarkMode}")]
        public bool UpdateUser(int id, bool isDarkMode)
        {
            return _data.UpdateDarkMode(id, isDarkMode);
        }

        // Delete User Account (soft delete)
        [HttpPost]
        [Route("DeleteUser")]
        public bool DeleteUser(UserModel userToDelete)
        {
            return _data.DeleteUser(userToDelete);
        }

        // Get All Users in Organization
        [HttpGet]
        [Route("GetUsersByOrganizationId/{organizationID}")]
        public IEnumerable<UserModel> GetUsersByOrganizationId(int organizationID)
        {
            return _data.GetUsersByOrganizationId(organizationID);
        }
    }
}