using Microsoft.AspNetCore.Mvc;
using SAMS.Models;
using SAMS.Services;

namespace SAMS.Controllers
{
    public class StudentController : Controller
    {
        private readonly MockApiService _mockApiService;

        public StudentController(MockApiService mockApiService)
        {
            _mockApiService = mockApiService;
        }

        public IActionResult StudentPage()
        {

            return View();
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            var model = new Users();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Users model)
        {
            ModelState.Remove("id");
            ModelState.Remove("User_ID");
            ModelState.Remove("Role");
            ModelState.Remove("Pass");
            ModelState.Remove("IsActive");

            if (!ModelState.IsValid)
                return View(model);

            var createdTeacher = await _mockApiService.CreateStudentAsync(model);

            if (createdTeacher == null)
            {
                ModelState.AddModelError("", "Failed to create student");
                return View(model);
            }

            return RedirectToAction("StudentPage", "Student"); // or wherever you list teachers
        }
    }
}
