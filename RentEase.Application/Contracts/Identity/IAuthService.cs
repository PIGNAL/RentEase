using RentEase.Application.Models.Identity;

namespace RentEase.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(AuthRequest request);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
