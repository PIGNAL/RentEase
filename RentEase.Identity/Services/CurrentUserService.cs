using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RentEase.Application.Contracts;

namespace RentEase.Identity.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserName => GetClaimValue(ClaimTypes.Name);

        public string Email => GetClaimValue(ClaimTypes.Email);

        public string FullName => GetClaimValue(ClaimTypes.GivenName);

        public string Address => GetClaimValue(ClaimTypes.StreetAddress);

        private string GetClaimValue(string claimType)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null || httpContext.User == null)
                return "System";

            var claim = httpContext.User.FindFirst(claimType);
            return claim?.Value ?? "System";
        }
    }
}
