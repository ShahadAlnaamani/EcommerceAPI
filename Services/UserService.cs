using EcommerceTask.DTOs;
using EcommerceTask.Models;
using EcommerceTask.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data;
using System.Numerics;
using System.Security.Claims;
using System.Text;

namespace EcommerceTask.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userrepository;

        public UserService(IUserRepository userrepo)
        {
            _userrepository = userrepo;
        }

        //Adding new user, converts userInDTO --> User
        public int AddUser(UserInDTO user)
        {
            //var hashed = PassHasher(admin.Password);

            var Newuser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Role = Role.NormalUser,
                AccountActive = true,
                Created = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
            };
            return _userrepository.AddUser(Newuser);
        }

        
        //Login function uses email and password [returns User]
        public User Login(string email, string password)
        {
            //var pswd = PassHasher(password);
            return _userrepository.GetUserByEmail(email, password);
        }


        //Password hashing function used to hash before storage and before comparing login pass to stored pass 
        //!Not done yet - error caused bec of random salting will update!
        public string PassHasher(string password)
        {
            // Generate a salt (bcrypt handles salting automatically)
            //string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the password with the salt using bcrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password); //, salt

            return hashedPassword;
        }


        //Function used to get info of current account [returns userOutDTO]
        public UserOutDTO GetMyDetails(int ID)
        {
            var user = _userrepository.GetUserByID(ID);

            var output = new UserOutDTO
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return output;
        }

        //----------------------------------The following are for admin use only-----------------------------//
        
        //Returns all user DTOs (to prevent oversharing of information)
        public List<UserOutDTO> GetAllUsers()
        {
            var users = _userrepository.GetAllUsers();
            var Outusers = new List<UserOutDTO>();

            //Mapping user -> UserOutDTO
            foreach (var user in users)
            {
                var output = new UserOutDTO
                {
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                };

                Outusers.Add(output);
            }

            return Outusers;
        }


        //Add new admin UserInDTO --> User
        public int AddAdmin(UserInDTO admin)
        {
            //var hashed = PassHasher(admin.Password);

            var user = new User
            {
                Name = admin.Name,
                Email = admin.Email,
                Password = admin.Password,
                PhoneNumber = admin.PhoneNumber,
                Role = Role.Admin,
                AccountActive = true,
                Created = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
            };
            return _userrepository.AddUser(user);
        }


        //Allows Admin to search for users by ID [User DTO]
        public UserOutDTO GetUserByID(int ID)
        {
            var user = _userrepository.GetUserByID(ID);

            //Mapping user -> UserOutDTO
            var output = new UserOutDTO
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return output;
        }


        //Allows Admins to search for users by name [UserDTOs]
        public List<UserOutDTO> GetUserByName(string Name)
        {
            var users = _userrepository.GetUsersByName(Name);
            var OutUsers = new List<UserOutDTO>();

            //Mapping user -> UserOutDTO
            foreach (var user in users)
            {
                var output = new UserOutDTO
                {
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                };

                OutUsers.Add(output);
            }

            return OutUsers;
        }

        //Allows Admin to search for users by phone no [User DTO]
        public UserOutDTO GetUserByPhoneNo(string phone)
        {
            var user = _userrepository.GetUserByPhone(phone);

            //Mapping user -> UserOutDTO
            var output = new UserOutDTO
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return output;
        }

    }
}
