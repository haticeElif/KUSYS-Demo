using KUSYS_Demo.Services.Abstract;
using KUSYS_Demo.WebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KUSYS_Demo.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel)
        {
            if (!string.IsNullOrEmpty(loginModel.Username) && string.IsNullOrEmpty(loginModel.Password))
            {
                return RedirectToAction("Login");
            }
            ClaimsIdentity identity = null;
            bool isAuthenticate = false;


            var userRole = _userService.Find(loginModel.Username, loginModel.Password);
            if (userRole.Result != null)
            {
                if (userRole.Result.Role.Role == "Admin")
                {
                    identity = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name,loginModel.Username),
                    new Claim(ClaimTypes.Role,"Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
                else
                {
                    identity = new ClaimsIdentity(new[]
                   {
                    new Claim(ClaimTypes.Name,loginModel.Username),
                    new Claim(ClaimTypes.NameIdentifier,userRole.Result.StudentId.ToString()),
                    new Claim(ClaimTypes.Role,"User")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
            }

            //if (loginModel.Username == "admin" && loginModel.Password == "a")
            //{
            //    identity = new ClaimsIdentity(new[]
            //    {
            //        new Claim(ClaimTypes.Name,loginModel.Username),
            //        new Claim(ClaimTypes.Role,"Admin")
            //    }, CookieAuthenticationDefaults.AuthenticationScheme);
            //    isAuthenticate = true;
            //}
            //if (loginModel.Username == "demo" && loginModel.Password == "c")
            //{
            //    identity = new ClaimsIdentity(new[]
            //    {
            //        new Claim(ClaimTypes.Name,loginModel.Username),
            //        new Claim(ClaimTypes.Role,"User")
            //    }, CookieAuthenticationDefaults.AuthenticationScheme);
            //    isAuthenticate = true;
            //}
            if (isAuthenticate)
            {
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("StudentFullList", "StudentCourse");
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
