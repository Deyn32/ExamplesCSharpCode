using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZooFerma.Models.Dto;
using ZooFerma.Services.Dto;

namespace ZooFerma.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextFactory _HttpContextFactory;

        public AuthController(IAuthService authService, IHttpContextFactory httpContextFactory) 
        {
            _authService = authService;
            _HttpContextFactory = httpContextFactory;
        }

        [HttpPost("signin")]
        public async Task<IResult> SignInAsync([FromBody] SignInRequest request)
        {
            HttpContext context = _HttpContextFactory.Create(HttpContext.Features);
            return await _authService.SignInAsync(request, context);
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IResult> GetCurrentUser()
        {
            var users = User.Claims.Select(x => new UserClaim(x.Type, x.Value)).ToList();
            return await _authService.GetUser(users);
        }

        [Authorize]
        [HttpGet("signout")]
        public async Task SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
