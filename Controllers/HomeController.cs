using Microsoft.AspNetCore.Mvc;
using SAMS.Models;
using SAMS.Services;
using SAMS.ViewModels;
using System.Diagnostics;

namespace SAMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MockApiService _mockApiService;

        public HomeController(ILogger<HomeController> logger, MockApiService mockApiService)
        {
            _logger = logger;
            _mockApiService = mockApiService;
        }

        [HttpGet]
        public IActionResult LoginPage()
        {
            var model = new LoginViewModel();
            return View(model); // ASP.NET will look for Views/Home/LoginPage.cshtml by default
        }

        [HttpPost]
        public async Task<IActionResult> LoginPage(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _mockApiService.LoginAsync((int)model.User_ID, model.Pass);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid ID or password");
                return View(model);
            }

            if (!user.IsActive)
            {
                TempData["ShowErrorModal"] = "true";
                return View(model);
            }

            return RedirectToAction("HomePage", "Home");
        }

        public IActionResult HomePage()
        {
            return View("~/Views/Home/HomePage.cshtml");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
