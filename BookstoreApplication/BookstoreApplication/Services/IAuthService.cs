using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto data);
        Task LoginAsync(LoginDto data);
    }
}