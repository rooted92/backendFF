using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using backendFF.Controllers;
using backendFF.Models;
using backendFF.Models.DTO;
using backendFF.Services.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace backendFF.Services
{
    public class UserService : ControllerBase
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public bool DoesUserExist(string? email)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Email == email && !user.IsDeleted) != null;
        }

        public OrganizationModel GetOrganizationByJoinCode(string joinCode)
        {
            return _context.OrganizationInfo.SingleOrDefault(organization => organization.JoinCode == joinCode && !organization.IsDeleted);
        }

        public string AddUser(CreateAccountDTO userToAdd)
        {
            OrganizationModel foundOrg = GetOrganizationByJoinCode(userToAdd.OrganizationJoinCode);
            if(foundOrg == null)
            {
                return "Incorrect Organization Code";
            }
            // if the user already exists
            string result = "User Already Exists";
            // if they do not exist we can then have the account be created
            if(!DoesUserExist(userToAdd.Email))
            {
                
                // creating a new instance of user model (empty object)
                UserModel newUser = new UserModel();
                // create our salt and hash password
                var hashPassword = HashPassword(userToAdd.Password);

                newUser.ID = 0;
                newUser.Name = userToAdd.Name;
                newUser.Email = userToAdd.Email;
                newUser.Salt = hashPassword.Salt;
                newUser.Hash = hashPassword.Hash;
                newUser.PhoneNumber = userToAdd.PhoneNumber;
                newUser.OrganizationID = foundOrg.ID;
                newUser.AccountType = userToAdd.AccountType;
                newUser.IsDarkMode = false;
                newUser.IsDeleted = false;
                // adding newUser to our database
                _context.Add(newUser);
                if(_context.SaveChanges() != 0)
                {
                    result = "User Account Created";
                }
                else
                {
                    result = "Error Nothing Saved To Table";
                }
            }

            return result;
        }

        public PasswordDTO HashPassword(string? password)
        {
            PasswordDTO newHashedPassword = new PasswordDTO();
            byte[] saltByte = new byte[64];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltByte);
            var Salt = Convert.ToBase64String(saltByte);

            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltByte, 10000);

            var Hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            newHashedPassword.Salt = Salt;
            newHashedPassword.Hash = Hash;

            return newHashedPassword;
        }

        public bool VerifyUserPassword(string? password, string? storedHash, string? storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            var newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return newHash == storedHash;
        }

        public IActionResult Login(LoginDTO user)
        {
            IActionResult result = Unauthorized();

            if(DoesUserExist(user.Email))
            {
                UserModel foundUser = GetUserByEmail(user.Email);

                if(VerifyUserPassword(user.Password, foundUser.Hash, foundUser.Salt))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokenOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signInCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    result = Ok(new { Token = tokenString });
                }
            }
            
            return result;
        }

        public UserInfoDTO GetUserInfo(string email)
        {
            UserModel foundUser = GetUserByEmail(email);
            UserInfoDTO userInfo = new UserInfoDTO();
            userInfo.ID = foundUser.ID;
            userInfo.Name = foundUser.Name;
            userInfo.Email = foundUser.Email;
            userInfo.PhoneNumber = foundUser.PhoneNumber;
            userInfo.OrganizationID = foundUser.OrganizationID;
            userInfo.AccountType = foundUser.AccountType;
            userInfo.IsDarkMode = foundUser.IsDarkMode;

            return userInfo;
        }
         public UserInfoDTO GetUserInfoByID(int userID)
        {
            UserModel foundUser = GetUserById(userID);
            UserInfoDTO userInfo = new UserInfoDTO();
            userInfo.ID = foundUser.ID;
            userInfo.Name = foundUser.Name;
            userInfo.Email = foundUser.Email;
            userInfo.PhoneNumber = foundUser.PhoneNumber;
            userInfo.OrganizationID = foundUser.OrganizationID;
            userInfo.AccountType = foundUser.AccountType;
            userInfo.IsDarkMode = foundUser.IsDarkMode;

            return userInfo;
        }

        public UserModel GetUserByEmail(string? email)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Email == email && !user.IsDeleted);
        }

        public bool UpdateUser(UserModel userToUpdate)
        {
            _context.Update<UserModel>(userToUpdate);
            return _context.SaveChanges() != 0;
        }

        public bool UpdateUserEmail(int id, string email)
        {
            bool result = false;
            if(GetUserByEmail(email) != null) {
                return false;
            }
            UserModel foundUser = GetUserById(id);
            if(foundUser != null)
            {
                foundUser.Email = email;
                result = UpdateUser(foundUser);
                // _context.Update<UserModel>(foundUser);
                // result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public UserModel GetUserById(int id)
        {
            return _context.UserInfo.SingleOrDefault(user => user.ID == id && !user.IsDeleted);
        }

        public IEnumerable<UserModel> GetUsersByOrganizationId(int organizationID)
        {
            return _context.UserInfo.Where(user => user.OrganizationID == organizationID && !user.IsDeleted);
        }

        public bool UpdateUserPassword(int id, string password)
        {
            UserModel foundUser = GetUserById(id);
            bool result = false;
            if(foundUser != null)
            {
                var hashPassword = HashPassword(password);
                foundUser.Salt = hashPassword.Salt;
                foundUser.Hash = hashPassword.Hash;
                result = UpdateUser(foundUser);
                // _context.Update<UserModel>(foundUser);
                // result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public bool UpdateUserDarkMode(int id, bool isDarkMode)
        {
            UserModel foundUser = GetUserById(id);
            bool result = false;
            if(foundUser != null)
            {
                foundUser.IsDarkMode = isDarkMode;
                result = UpdateUser(foundUser);
                // _context.Update<UserModel>(foundUser);
                // result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public bool UpdateUserInfo(UserInfoDTO userToUpdate)
        {
            UserModel foundUser = GetUserById(userToUpdate.ID);
            foundUser.Name = userToUpdate.Name;
            foundUser.Email = userToUpdate.Email;
            foundUser.PhoneNumber = userToUpdate.PhoneNumber;
            foundUser.IsDarkMode = userToUpdate.IsDarkMode;
            
            return UpdateUser(foundUser);
        }

        public bool DeleteUser(int userID)
        {
            UserModel userToDelete = GetUserById(userID);
            userToDelete.IsDeleted = true;
            return UpdateUser(userToDelete);
        }
    }
}