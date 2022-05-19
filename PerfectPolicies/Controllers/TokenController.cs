using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PerfectPolicies.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///  controller responsible for handling Session & tokens functions.
/// </summary>
namespace PerfectPolicies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;


        /// <summary>
        /// retrieve configuration data & database connection
        /// </summary>

        public TokenController(IConfiguration config, ApplicationContext context)
        {
            _configuration = config;
            _context = context;
        }

 #region public Endpoints

        /// <summary>
        /// method to generate user's login token to authenticate his access to the application
        /// </summary>
        /// <param name="_userData"> userName & userPassword</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GenerateToken")]
        public IActionResult GenerateToken(UserInfo _userData)
        {
            // checks empty fields
            if (_userData != null && _userData.UserName != null && _userData.Password != null)
            {
                // retrieve the user for these credentials
                var user = GetUser(_userData.UserName, _userData.Password);

                // If we have a user that matches the credentials, then create sessoin token for him
                if (user != null)
                {
                    //create claims details based on the user information and JWT configurations
                    var claims = new[] {
                    // JWT Subject
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"]),
                    // JWT ID
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    // JWT Date/Time
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    // JWT User ID
                    new Claim("Id", user.UserInfoID.ToString()),

                   // new Claim(ClaimsIdentity);
                    // JWT UserName
                    new Claim("UserName", user.UserName)
                   };

                    // retrieve JWT key value from configuration file
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                    // use the generated key to generate new Signing Credentials
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // Generate a new token that will authenticate user's access
                    var token = new JwtSecurityToken(
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Audience"],
                        claims,
                        // How long the JWT will be valid for
                        expires: DateTime.UtcNow.AddDays(2),
                        signingCredentials: signIn);

                    // Return generated Token via JSON
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
#endregion


        #region Private Methods

        /// <summary>
        /// method to retrieve user data
        /// </summary>
        /// <param name="username">userName</param>
        /// <param name="passWord">Password</param>
        /// <returns>user object</returns>
        private UserInfo GetUser(string username, string passWord)
        {
            var user = _context.Users.Where(c => c.UserName.Equals(username)).FirstOrDefault();

            if (user != null && user.Password.Equals(passWord))
            {
                return user;
            }

            return null;

        }
        #endregion
    }
}
