using EcommerceTask.DTOs;
using EcommerceTask.Models;
using EcommerceTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceTask.Controllers
{
    [Authorize] //Ensures that specific functions are not used by unauthenticated users 
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;


        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;

        }


        //This function adds new NormalUsers and does not need authentication [returns new user ID]
        [AllowAnonymous]
        [HttpPost("AddUser")]
        public IActionResult AddUser(UserInDTO user)
        {
            try
            {
                return Ok(_userService.AddUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //This function authenticates users using emails and passwords [returns token]
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            User user = _userService.Login(email, password); //Getting user object using email and passowrd

            if (user != null) //user found --> token
            {
                //Generating token
                string token = GenerateJwtToken(user.UID.ToString(), user.Role.ToString());
                return Ok(token);

            }
            else //user not found --> error message
            {
                return BadRequest("<!>Invalid Credentials<!>");
            }
        }

        //This function returns current users (DTO) details using the token used to register 
        [HttpGet("GetMyDetails")]
        public IActionResult GetUserDetails()
        {
            try
            {
                //Getting the user ID from token 
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                //Returning details 
                return Ok(_userService.GetMyDetails(int.Parse(userId)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        ///----------------------------------The following are all admin only functions----------------------------------------// 
       
        //Gets all user (DTO) details to admins only 
        [HttpGet("ADMIN: GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(_userService.GetAllUsers()); //returns all users DTO information

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Adds a new admin can only be done through another admin account, creating a chain of trust (not most secure but is good enough for now) [returns admin ID]
        [HttpPost("ADMIN: AddAdmin")]
        public IActionResult AddAdmin(UserInDTO user)
        {
            try
            {
                return Ok(_userService.AddAdmin(user)); //Creating new admin
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Allows admins to search for specific accounts using ID (only authenticated admin accounts can use this function) [Returns user DTO]
        [HttpGet("ADMIN: GetUserByID {ID}")]
        public IActionResult GetUserByID(int ID)
        {
            try
            {
                var users = _userService.GetUserByID(ID); //Getting user 
                
                if (users == null) return BadRequest("<!>No users with this ID<!>");
                else return Ok(users);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Allows admins to search for specific accounts using name (only authenticated admin accounts can use this function) [Returns user DTO]
        [HttpGet("ADMIN: GetUserByName {Name}")]
        public IActionResult GetUserByName(string Name)
        {
            try
            {
                var users =  _userService.GetUserByName(Name); //Getting user 

                if (users == null) return BadRequest("<!>No users with this name<!>");
                else return Ok(users);  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //Allows admins to search for specific accounts using phone no(only authenticated admin accounts can use this function) [Returns user DTO]
        [HttpGet("ADMIN: GetUserByPhoneNo {PhoneNo}")]
        public IActionResult GetUserByPhoneNo(string phone)
        {
            try
            {
                var users = _userService.GetUserByPhoneNo(phone); //Getting user 

                if (users == null) return BadRequest("<!>No users with this phone number<!>");
                else return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //------------------------------Function that generates unique JWT tokens----------------------------//
        //Uses user ID (used for get my details) and user role (used for role based authorization)
        [NonAction]
        public string GenerateJwtToken(string userId, string userrole)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()), //adding new custom types
                new Claim(ClaimTypes.Role, userrole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
