using AuthUserIdentity.Models;
using AuthUserIdentity.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthUserIdentity.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Username,Password")] LoginViewModel loginviewmodel)
        {
            if (loginviewmodel.UserName == null || loginviewmodel.Password == null)
                return RedirectToAction("Index");

            var loginmaster = await _userService.AuthenticateAsync(loginviewmodel.UserName, loginviewmodel.Password);

            if (loginmaster == null || loginmaster.Username == null || loginmaster.Password == null)
                return RedirectToAction("Index");

            bool authenticated = false;

            ClaimsIdentity identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loginmaster.Id.ToString()),
                new Claim(ClaimTypes.Name, loginmaster.Username)
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            authenticated = true;

            if (authenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
                    IsPersistent = false,
                    AllowRefresh = false
                });

                HttpContext.Session.SetString("UserId", loginmaster.Id.ToString());
                return RedirectToAction("Index", "User");
            }

            ViewData["Message"] = "Wrong username or password. Try again!";

            return View(loginviewmodel);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UC");
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}