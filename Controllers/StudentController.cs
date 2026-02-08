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

        [HttpGet]
        public async Task<IActionResult> EditStudent(string id) // Change parameter to string
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            // Fetch the teacher from MockAPI
            var student = await _mockApiService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound(); // or redirect to the Teacher list with an error

            // Pass the full Users model to the view
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(Users model)
        {
            // Remove fields not included in form
            ModelState.Remove("id"); // optional
            ModelState.Remove("Pass");
            ModelState.Remove("Role");
            ModelState.Remove("IsActive");

            if (!ModelState.IsValid)
                return View(model);

            // Send the full object to MockAPI
            var success = await _mockApiService.UpdateUsersAsync(model); // pass full Users
            if (!success)
            {
                ModelState.AddModelError("", "Failed to update teacher.");
                return View(model);
            }

            TempData["Success"] = "Teacher updated successfully!";
            return RedirectToAction("StudentPage", "Student");
        }

        // GET: /Student/DeleteStudent/{id}
        [HttpGet]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            // Fetch the student from MockAPI
            var student = await _mockApiService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound();

            // Pass the student to the view
            return View(student);
        }

        // POST: /Student/DeleteStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStudent(Users model)
        {
            if (model == null || string.IsNullOrEmpty(model.id))
                return NotFound();

            // Fetch the student to ensure it exists
            var student = await _mockApiService.GetStudentByIdAsync(model.id);
            if (student == null)
                return NotFound();

            // Soft-delete by changing the Role
            student.Role = "Deleted";

            var success = await _mockApiService.UpdateUsersAsync(student);
            if (!success)
            {
                ModelState.AddModelError("", "Failed to delete student.");
                return View(model);
            }

            TempData["Success"] = "Student deleted successfully!";
            return RedirectToAction("StudentPage");
        }

    }
}
