using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZooFerma.Models.Dto;
using ZooFerma.Services.Dao;

namespace ZooFerma.Services.Dto.Impls
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly IUserService _userService;

        public AuthServiceImpl(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResult> SignInAsync(SignInRequest request, HttpContext context)
        {
            var user = _userService.GetUserByEmailAndPassword(email: request.email, password: request.password);
            if(user == null) 
            {
                return Results.BadRequest("Логин или пароль заданы не верно!");
            }

            var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Email, value: request.email),
                new Claim(type: ClaimTypes.Name, value: user.fio)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                });

            return Results.Ok("Вход разрешен!");
        }

        public async Task<IResult> GetUser(List<UserClaim> users)
        {
            return Results.Ok(users);
        }
    }
}
