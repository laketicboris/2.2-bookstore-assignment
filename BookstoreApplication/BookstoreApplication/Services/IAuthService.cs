using BookstoreApplication.DTOs;
using System.Security.Claims;

namespace BookstoreApplication.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto data);
        Task<string> LoginAsync(LoginDto data);
        Task<ProfileDto> GetProfile(ClaimsPrincipal userPrincipal);
    }
}