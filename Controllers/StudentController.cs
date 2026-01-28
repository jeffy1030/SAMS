using Microsoft.AspNetCore.Mvc;

namespace SAMS.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult StudentPage()
        {
            return View();
        }

        public IActionResult AddStudent()
        {
            return View();
        }
    }
}
