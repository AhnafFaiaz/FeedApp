using FeedApp.Core.Interfaces.IServices;
using FeedApp.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedApp.UI.Controllers
{
    public class SignupController : Controller
    {
        private readonly ILogger<SignupController> _logger;
        private readonly ISignupService _signupService;
        public SignupController(ILogger<SignupController> logger, ISignupService signupService)
        {
            _logger = logger;
            _signupService = signupService;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Signup(string username, string email, string password)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _signupService.Signup(username,email,password);
                    HttpContext.Session.SetInt32("UserID", data.UserID);
                    HttpContext.Session.SetString("UserName", data.UserName);
                    return RedirectToAction("Signin","Signin");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while adding the product");
                    ModelState.AddModelError("Error", $"An error occurred while adding the product- " + ex.Message);
                    return View();
                }
            }
            return View();
        }
    }
}
