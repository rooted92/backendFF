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

        // Add User
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

        // Get User Info After Login
        [HttpGet]
        [Route("GetUserInfo/{email}")]
        public UserInfoDTO GetUserInfo(string email)
        {
            return _data.GetUserInfo(email);
        }

        // Update User
        [HttpPost]
        [Route("UpdateUser")]
        public bool UpdateUser(UserModel userToUpdate)
        {
            return _data.UpdateUser(userToUpdate);
        }

        // Update User Email
        [HttpPost]
        [Route("UpdateUserEmail/{id}/{email}")]
        public bool UpdateUserEmail(int id, string email)
        {
            return _data.UpdateUserEmail(id, email);
        }

        // Update User Password
        [HttpPost]
        [Route("UpdateUserPassword/{id}/{password}")]
        public bool UpdateUserPassword(int id, string password)
        {
            return _data.UpdateUserPassword(id, password);
        }

        // Update User Dark Mode Setting
        [HttpPost]
        [Route("UpdateUserDarkMode/{id}/{isDarkMode}")]
        public bool UpdateUserDarkMode(int id, bool isDarkMode)
        {
            return _data.UpdateUserDarkMode(id, isDarkMode);
        }

        // Delete User Account (soft delete)
        [HttpPost]
        [Route("DeleteUser")]
        public bool DeleteUser(UserModel userToDelete)
        {
            return _data.DeleteUser(userToDelete);
        }

        [HttpGet]
        [Route("GetUserByEmail/{email}")]
        public UserModel GetUserByEmail(string email)
        {
            return _data.GetUserByEmail(email);
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