using Microsoft.AspNetCore.Mvc;
using SAMS.Models;
using SAMS.Services;

namespace SAMS.Controllers
{
    public class TeacherController : Controller
    {
        private readonly MockApiService _mockApiService;

        public TeacherController(MockApiService mockApiService)
        {
            _mockApiService = mockApiService;
        }

        public IActionResult TeacherPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddTeacher()
        {
            var model = new Users();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher(Users model)
        {
            ModelState.Remove("id");
            ModelState.Remove("User_ID");
            ModelState.Remove("Role");
            ModelState.Remove("Pass");

            if (!ModelState.IsValid)
                return View(model);

            var createdTeacher = await _mockApiService.CreateTeacherAsync(model);

            if (createdTeacher == null)
            {
                ModelState.AddModelError("", "Failed to create teacher");
                return View(model);
            }

            return RedirectToAction("TeacherPage", "Teacher"); // or wherever you list teachers
        }

        //[HttpPost]
        //public IActionResult AddTeacher()
        //{
        //    return View();
        //}
    }
}
