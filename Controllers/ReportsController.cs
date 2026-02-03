using Microsoft.AspNetCore.Mvc;
using SAMS.Services;

namespace SAMS.Controllers
{
    public class ReportsController : Controller
    {
        private readonly MockApiService _mockApiService;
        public ReportsController(MockApiService mockApiService)
        {
            _mockApiService = mockApiService;
        }

        public IActionResult ReportsPage()
        {
            return View();
        }
    }
}
