using FeedApp.Core.Entities.General;
using FeedApp.Core.Interfaces.IServices;
using FeedApp.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedApp.UI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IDashboardService _dashboardService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardController(ILogger<DashboardController> logger, IDashboardService dashboardService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _dashboardService = dashboardService;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> AdminDashboard()
        {
            
            //var dash = await _dashboardService.AdminDashboard();
            //return View(dash);
            return View();

        }
        [HttpGet]
        [Authorize(Roles="User")]
        public async Task<IActionResult> UserDashboard()
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserID") ?? 0; // Default value of 0 if UserID is not set
            var dash = await _dashboardService.UserDashboard(userId);
            return View(dash);
        }


    }
}
