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

        public string UserName
        {
            get
            {
                var result = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name);
                return result?.Value??string.Empty;
            }
        }

        public string Email
        {
            get
            {
                var result = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email);
                return result?.Value ?? string.Empty;
            }
        }

        public string FullName {
            get
            {
                var result = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.GivenName);
                return result?.Value ?? string.Empty;
            }
        }
        public string Address {
            get
            {
                var result = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.StreetAddress);
                return result?.Value ?? string.Empty;
            }
        }
    }
}
