using Microsoft.AspNetCore.Http;
using RentEase.Application.Contracts;

namespace RentEase.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserName =>
            _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
    }
}
