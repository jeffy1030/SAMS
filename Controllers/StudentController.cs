using Microsoft.AspNetCore.Mvc;
using SAMS.Models;
using SAMS.Services;
using System.Threading.Tasks;

namespace SAMS.Controllers
{
    public class StudentController : Controller
    {
        private readonly MockApiService _mockApiService;

        public StudentController(MockApiService mockApiService)
        {
            _mockApiService = mockApiService;
        }

        public async Task<IActionResult> StudentPage()
        {
            var students = await _mockApiService.GetStudentAsync();
            return View(students);
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

        public async Task<IActionResult> EditTeacher(int id)
        {
            ModelState.Remove("id");
            ModelState.Remove("User_ID");
            ModelState.Remove("Role");
            ModelState.Remove("Pass");
            ModelState.Remove("IsActive");

            // Use MockApiService to fetch the teacher
            var student = await _mockApiService.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound(); // Or redirect to a list page with error
            }

            // Map teacher to ViewModel if needed
            var model = new Users
            {
                User_ID = student.User_ID,
                FName = student.FName,
                LName = student.LName,
                MName = student.MName,
                Email = student.Email,
                Gender = student.Gender
                // Add any other fields needed for editing
            };

            return View(model);
        }
    }
}
