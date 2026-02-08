using Microsoft.AspNetCore.Mvc;
using SAMS.Models;
using SAMS.Services;
using SAMS.ViewModels;
using System.Threading.Tasks;

namespace SAMS.Controllers
{
    public class TeacherController : Controller
    {
        private readonly MockApiService _mockApiService;

        public TeacherController(MockApiService mockApiService)
        {
            _mockApiService = mockApiService;
        }

        public async Task<IActionResult> TeacherPage()
        {
            var teachers = await _mockApiService.GetTeachersAsync();
            return View(teachers);
        }

        [HttpGet]
        public IActionResult AddTeacher()
        {        
            return View(new Users());
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher(Users model)
        {            
            ModelState.Remove("id");
            ModelState.Remove("User_ID");
            ModelState.Remove("Role");
            ModelState.Remove("Pass");
            ModelState.Remove("IsActive");

            if (!ModelState.IsValid)
                return View(model);

            var createdTeacher = await _mockApiService.CreateTeacherAsync(model);

            if (createdTeacher == null)
            {
                TempData["ShowErrorModal"] = "true";
            }
            else
            {
                TempData["ShowSuccessModal"] = "true";
            }

            return View(model);
        }

        // GET: /Teacher/EditTeacher/{id}
        [HttpGet]
        public async Task<IActionResult> EditTeacher(string id) // Change parameter to string
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            // Fetch the teacher from MockAPI
            var teacher = await _mockApiService.GetTeacherByIdAsync(id);
            if (teacher == null)
                return NotFound(); // or redirect to the Teacher list with an error

            // Pass the full Users model to the view
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeacher(Users model)
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
            return RedirectToAction("TeacherPage", "Teacher");
        }
    }
}
