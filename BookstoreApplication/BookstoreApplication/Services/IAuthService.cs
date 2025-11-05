using System.Security.Claims;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto data);
        Task<string> LoginAsync(LoginDto data);
        Task<ProfileDto> GetProfile(ClaimsPrincipal userPrincipal);
        Task<string> LoginWithGoogleAsync(ClaimsPrincipal googlePrincipal);
        Task<ApplicationUser> CreateGoogleUserAsync(string email, string? fullName);
    }
}