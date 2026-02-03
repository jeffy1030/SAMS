using Microsoft.AspNetCore.Mvc;

namespace SAMS.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult AttendancePage()
        {
            return View();
            
        }

        public IActionResult AddAttendance()
        {
            return View();
        }
    }
}
