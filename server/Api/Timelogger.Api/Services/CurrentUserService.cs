using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using Timelogger.Domain.Abstractions;

namespace Timelogger.Api.Services
{
     public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Username => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        //public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue("sub");
       
        //Hardcoded guid as we don't have any authentication setup
        public string UserId => "8b0dbbd3-02c2-4ce6-9409-90aa201366b1";
    }
}
