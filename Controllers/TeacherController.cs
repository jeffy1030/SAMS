using Microsoft.AspNetCore.Mvc;

namespace SAMS.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult TeacherPage()
        {
            return View();
        }

        public IActionResult AddTeacher()
        {
            return View();
        }
    }
}
