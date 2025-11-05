using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace BookstoreApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration configuration, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task RegisterAsync(RegistrationDto data)
        {
            var user = _mapper.Map<ApplicationUser>(data);

            user.DateOfBirth = DateTime.SpecifyKind(user.DateOfBirth, DateTimeKind.Utc);

            var result = await _userManager.CreateAsync(user, data.Password);
            if (!result.Succeeded)
            {
                string errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new BadRequestException(errorMessage);
            }

            await _userManager.AddToRoleAsync(user, "Librarian");
        }

        public async Task<string> LoginAsync(LoginDto data)
        {
            var user = await _userManager.FindByNameAsync(data.Username);
            if (user == null)
            {
                throw new BadRequestException("Invalid credentials.");
            }

            var passwordMatch = await _userManager.CheckPasswordAsync(user, data.Password);
            if (!passwordMatch)
            {
                throw new BadRequestException("Invalid credentials.");
            }

            var token = await GenerateJwt(user);
            return token;
        }

        public async Task<ProfileDto> GetProfile(ClaimsPrincipal userPrincipal)
        {
            var username = userPrincipal.FindFirstValue("username");

            if (username == null)
            {
                throw new BadRequestException("Token is invalid");
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new NotFoundException("User with provided username does not exist");
            }

            return _mapper.Map<ProfileDto>(user);
        }

        private async Task<string> GenerateJwt(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim("username", user.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim("role", role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<string> LoginWithGoogleAsync(ClaimsPrincipal googlePrincipal)
        {
            var email = googlePrincipal.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
                throw new GoogleAuthException("Email not provided by Google");

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                var name = googlePrincipal.FindFirstValue(ClaimTypes.Name);
                user = await CreateGoogleUserAsync(email, name);
                _logger.LogInformation("Created new Google user: {Email}", email);
            }
            else
            {
                _logger.LogInformation("Existing user logged in via Google: {Email}", email);
            }

            return await GenerateJwt(user);
        }

        public async Task<ApplicationUser> CreateGoogleUserAsync(string email, string? fullName)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                Name = ExtractFirstName(fullName),
                Surname = ExtractLastName(fullName),
                DateOfBirth = DateTime.SpecifyKind(new DateTime(1990, 1, 1), DateTimeKind.Utc)
            };

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new UserCreationException($"Failed to create Google user: {errors}");
            }

            await _userManager.AddToRoleAsync(user, "Librarian");
            return user;
        }

        private static string ExtractFirstName(string? fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return "Google";

            var parts = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts.Length > 0 ? parts[0] : "Google";
        }

        private static string ExtractLastName(string? fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return "User";

            var parts = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : "User";
        }
    }
}