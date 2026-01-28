using Microsoft.AspNetCore.Mvc;

namespace SAMS.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult ReportsPage()
        {
            return View();
        }
    }
}
