using BLL.Abstract;
using BLL.Models;
using DAL.Data;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            AppDbContext dbContext,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<LoginResponseModel> LoginAsync(LoginRequestModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new LoginResponseModel
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                return new LoginResponseModel
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            // Check if user is locked out
            if (await _userManager.IsLockedOutAsync(user))
            {
                return new LoginResponseModel
                {
                    Success = false,
                    Message = "Account is locked out. Please try again later."
                };
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);

            return new LoginResponseModel
            {
                Success = true,
                Message = "Login successful.",
                AccessToken = token,
                User = new UserInfoModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Patronymic = user.Patronymic,
                    Email = user.Email ?? string.Empty,
                    PhoneNumber = user.PhoneNumber ?? string.Empty,
                    RoleId = user.RoleId
                }
            };
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtSecretKey = _configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey is not configured");
            var jwtIssuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer is not configured");
            var jwtAudience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience is not configured");
            var expirationMinutes = int.Parse(_configuration["Jwt:ExpirationInMinutes"] ?? "60");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, $"{user.Name} {user.Surname}"),
                new Claim("userId", user.Id.ToString()),
                new Claim("roleId", user.RoleId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> RegisterAsync(RegisterRequestModel request)
        {
            // Check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return false;
            }

            // Validate role exists
            var roleExists = await _dbContext.Roles.AnyAsync(r => r.RoleId == request.RoleId);
            if (!roleExists)
            {
                return false;
            }

            // Admin identifier is stored without validation
            // If validation is needed, it can be implemented by checking against a database table or configuration

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Name = request.Name,
                Surname = request.Surname,
                Patronymic = request.Patronymic,
                RoleId = request.RoleId,
                AdminIdentifier = request.AdminIdentifier,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> LogoutAsync()
        {
            // With JWT tokens, logout is handled client-side by removing the token
            // Server-side logout is not necessary for stateless JWT tokens
            return true;
        }
    }
}

