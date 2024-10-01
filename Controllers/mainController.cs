using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Repositories;
using WebApi.Helpers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Controllers
{
    public class mainController : Controller
    {
        public mainRepo myRepo;
        private DataContext db;
        private readonly IConfiguration _config;

        public mainController(DataContext T, IConfiguration config)
        {
            myRepo = new mainRepo(T);
            db = T;
            _config = config;
        }

        // Get
        // Get FAQ

        [HttpGet("getDepartmentList")]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {
                var result = myRepo.getDepartment();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getSubjectList/{department}")]
        public async Task<IActionResult> GetAllSubjects(string department)
        {
            try
            {
                var result = myRepo.getSubjects(department);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getFAQList/{department}")]
        public async Task<IActionResult> GetAllFAQ(string department)
        {
            try
            {
                var result = myRepo.getFAQ(department);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getGroupsList/{department}")]
        public async Task<IActionResult> GetAllGroups(string department)
        {
            try
            {
                var result = myRepo.getGroups(department);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // Login and register 
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {

                var existingUser = db.MainTable.Any(u => u.UserName == request.UserName);
                if (existingUser)
                {
                    return BadRequest("User already exists.");
                }

                // Create a new user object
                var newUser = new MainTable
                {
                    UserName = request.UserName,
                    Password = HashPassword(request.Password),
                    ProfilePicture = "N/A",
                    Name = "N/A",
                    Department = "N/A",
                    Type = request.Type,
                };

                // Generate a unique verification code
                var verificationCode = GenerateVerificationCode();

                // Store the verification code and expiration in the new user object
                newUser.PasswordResetCode = verificationCode;
                newUser.PasswordResetExpiration = DateTime.UtcNow.AddMinutes(15); // Code expires in 15 minutes

                db.MainTable.Add(newUser);
                db.SaveChanges();

                // Send the verification code via email
                var emailSubject = "EduvosApp: Email Verification";
                var emailBody = $@"
                <p>Hello {newUser.UserName},</p>
                <p>Thank you for registering. Please use the verification code below to verify your email:</p>
                <h3>{verificationCode}</h3>
                <p>This code will expire in 15 minutes.</p>";

                var sendEmail = myRepo.SendEmailToPerson(emailSubject, emailBody, newUser.UserName, "", "");

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("An error occurred while processing your request.");
            }
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
        {
            try
            {
                var user = db.MainTable.SingleOrDefault(u => u.UserName == request.Email && u.PasswordResetCode == request.VerificationCode);
                if (user == null || user.PasswordResetExpiration < DateTime.UtcNow)
                {
                    return BadRequest("Invalid or expired verification code.");
                }

                // Activate the user's account
                user.PasswordResetCode = null; // Clear the verification code
                user.PasswordResetExpiration = DateTime.Now; // Clear the expiration time
                db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("An error occurred while processing your request.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = db.MainTable.SingleOrDefault(u => u.UserName == request.Username);

                if (user == null || !VerifyPasswordHash(request.Password, user.Password))
                {
                    return Unauthorized("Invalid username or password.");
                }

                // Generate JWT and Refresh Token
                var accessToken = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();
                var tokenExpiration = DateTime.UtcNow.AddDays(7);

                // Save refresh token to user and set expiration for 7 days
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiration = tokenExpiration; // 7-day refresh token expiry
                db.SaveChanges();

                // Set the refresh token in a secure, HttpOnly cookie
                Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
                {
                    HttpOnly = true, // Prevent JavaScript access
                    Secure = true, // Ensure it's only sent over HTTPS = true, if testing locally make false
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7) // Refresh token expires in 7 days
                });

                return Ok(new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    Username = user.UserName,
                    ProfilePicture = user.ProfilePicture,
                    Id = user.Id,
                    Department = user.Department,
                    UserType = user.Type,
                    RefreshTokenExpiration = tokenExpiration
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            try
            {
                // Retrieve the refresh token from the request body
                var refreshToken = tokenRequest.RefreshToken;
                var username = tokenRequest.Username;

                // Find the user associated with this refresh token
                var user = db.MainTable.SingleOrDefault(u => u.UserName == username && u.RefreshToken == refreshToken);

                if (user == null)
                {
                    return Unauthorized("Invalid or expired refresh token.");
                }

                // Generate new JWT and refresh tokens
                var newAccessToken = GenerateJwtToken(user);
                var newRefreshToken = GenerateRefreshToken();
                var newTokenExpiration = DateTime.UtcNow.AddDays(7);

                // Update the refresh token in the database
                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiration = newTokenExpiration; // New expiration for refresh token
                db.SaveChanges();

                // Set the refresh token in a secure, HttpOnly cookie with the NEW refresh token
                Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
                {
                    HttpOnly = true, // Prevent JavaScript access
                    Secure = true, // Ensure it's only sent over HTTPS (for production) set to false for local testing
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7) // Refresh token expires in 7 days
                });

                // Return new tokens
                return Ok(new
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken, // Return the new refresh token
                    RefreshTokenExpiration = newTokenExpiration
                });
            }
            catch (Exception e)
            {
                return BadRequest("Failed to refresh token: " + e.Message);
            }
        }

        private string GenerateJwtToken(MainTable user)
        {
            var key = _config["Jwt:Key"]; // Get secret key from appsettings
            var keyBytes = Encoding.UTF8.GetBytes(key); // Convert it to byte array
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15), // Access token expiration (15 minutes)
                Issuer = _config["Jwt:Issuer"], // Use issuer from appsettings.json
                Audience = _config["Jwt:Audience"], // Use audience from appsettings.json
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber); // Return a base64-encoded refresh token
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false, // We are validating manually
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"]
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var computedHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return computedHash == storedHash;
            }
        }

        // Forget password / Reset
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] GenerateVerificationCode request)
        {
            try
            {
                var user = db.MainTable.SingleOrDefault(u => u.UserName == request.Email);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                // Generate a unique verification code
                var verificationCode = GenerateVerificationCode();

                // Store the verification code (this assumes your UserTable has a field for it)
                user.PasswordResetCode = verificationCode;
                user.PasswordResetExpiration = DateTime.UtcNow.AddMinutes(15); // Code expires in 15 minutes
                db.SaveChanges();

                // Send the verification code via email
                var emailSubject = "EduvosApp: Password Reset Request";
                var emailBody = $@"
                <p>Hello {user.UserName},</p>
                <p>You requested a password reset. Use the verification code below to reset your password:</p>
                <h3>{verificationCode}</h3>
                <p>This code will expire in 15 minutes.</p>
                <p>If you did not request a password reset, please ignore this email.</p>";

                var sendEmail = myRepo.SendEmailToPerson(emailSubject, emailBody, user.UserName, "", "");

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("An error occurred while processing your request.");
            }
        }

        private string GenerateVerificationCode()
        {
            var rng = new Random();
            var code = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                code.Append(rng.Next(0, 10));
            }
            return code.ToString();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                var user = db.MainTable.SingleOrDefault(u => u.UserName == request.Email && u.PasswordResetCode == request.VerificationCode);
                if (user == null || user.PasswordResetExpiration < DateTime.UtcNow)
                {
                    return BadRequest("Invalid or expired verification code.");
                }

                // Hash the new password
                user.Password = HashPassword(request.NewPassword);
                user.PasswordResetCode = null; // Clear the reset code
                user.PasswordResetExpiration = DateTime.Now; // Clear the expiration time
                db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("An error occurred while processing your request.");
            }
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class TokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Username { get; set; }
    }

    public class GenerateVerificationCode
    {
        public string Email { get; set; }
    }

    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
    }

    public class VerifyEmailRequest
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
    }

    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
        public string NewPassword { get; set; }
    }
}

