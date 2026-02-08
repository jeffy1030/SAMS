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

        //[HttpPost]
        //public IActionResult AddTeacher()
        //{
        //    return View();
        //}
    }
}
