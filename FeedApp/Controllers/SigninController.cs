using FeedApp.Core.Interfaces.IServices;
using FeedApp.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Security.Claims;

namespace FeedApp.UI.Controllers
{
    public class SigninController : Controller
    {

        private readonly ILogger<SigninController> _logger;
        private readonly ISigninService _signinService;
        //public readonly IDashboardService _dashboardService;
        public SigninController(ILogger<SigninController> logger, ISigninService signinService)
        {
            _logger = logger;
            _signinService = signinService;
            //_dashboardService = dashboardService;
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(string email, string password)
        {
            var user = await _signinService.Signin(email, password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserID", user.UserID.ToString());
                HttpContext.Session.SetString("UserName", user.UserName);

                // Determine and set the role based on RoleID.
                var role = "User"; // Default to "User"
                if (user.RoleID == 1)
                {
                    role = "Admin";
                }

                // Set the role in the user's claims.
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, role) // Set the role
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Sign in the user with the role.
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                switch (role)
                {
                    case "Admin":
                        return RedirectToAction("ProblemIndex", "Problem");
                    case "User":
                        return RedirectToAction("MyProblems", "Problem");
                    default:
                        return RedirectToAction("Signin");
                }
            }
            else
            {
                return RedirectToAction("Signin");
            }
        }



        //public async Task<IActionResult> Signin(string email, string password)
        //{
        //    var user = await _signinService.Signin(email, password);

        //    if (user != null)
        //    {
        //        HttpContext.Session.SetString("UserID", user.UserID.ToString());
        //        HttpContext.Session.SetString("UserName", user.UserName);
        //        //HttpContext.Session.SetString("UserID", user.UserID.ToString());
        //        //TempData["UserName"] = user.UserName;
        //        switch (user.RoleID)
        //        {
        //            case 1: // Admin hishabe dekhabe
        //                return RedirectToAction("ProblemIndex","Problem");
        //            case 2: // Normal user hishabe dekhabe
        //                return RedirectToAction("MyProblems","Problem");
        //            default:
        //                return RedirectToAction("Signin");
        //        }
        //    }
        //    else { return RedirectToAction("Signin"); }



        //}

        [HttpGet]
        
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserID");
            HttpContext.SignOutAsync();
            return RedirectToAction("Signin");
        }



    }
}
