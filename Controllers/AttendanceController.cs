using Microsoft.AspNetCore.Mvc;
using SAMS.Models;
using SAMS.Services;
using SAMS.ViewModels;
using System.Threading.Tasks;

namespace SAMS.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly MockApiService _mockApiService;
        public AttendanceController(MockApiService mockApiService)
        {
            _mockApiService = mockApiService;
        }

        public async Task<IActionResult> AttendancePage()
        {
            // Fetch all attendance sessions from MockAPI
            var attendances = await _mockApiService.GetAttendancesAsync();

            // Pass the attendance list to the view
            return View(attendances);
        }

        public async Task<IActionResult> AddAttendance()
        {
            var users = await _mockApiService.GetUsersAsync();

            var students = users
                .Where(u => u.Role == "Student")
                .Select(u => new StudentAttendanceItemVM
                {
                    User_ID = u.User_ID,          // preserve User_ID
                    StudentName = $"{u.LName}, {u.FName} {(string.IsNullOrEmpty(u.MName) ? "-" : u.MName.Substring(0, 1))}.",
                    Status = "Absent"
                })
                .ToList();

            var vm = new CreateAttendanceVM
            {
                Date = System.DateTime.Today,
                Students = students
            };

            return View(vm);
        }

        // POST: Save Attendance
        [HttpPost]
        public async Task<IActionResult> AddAttendance(CreateAttendanceVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var attendance = new Attendance
            {
                Name = vm.Name,
                Date = vm.Date,
                Records = vm.Students.Select(s => new AttendanceRecord
                {
                    User_ID = s.User_ID,   // keep User_ID from Users
                    Status = s.Status
                }).ToList()
            };

            await _mockApiService.SaveAttendanceAsync(attendance);

            return RedirectToAction("AttendancePage");
        }

        // GET: View attendance details
        //public async Task<IActionResult> AttendanceDetails(int attId)
        //{
        //    var attendance = await _mockApiService.GetAttendanceByIdAsync(attId);

        //    if (attendance == null)
        //        return NotFound();

        //    // Map records to StudentAttendanceItemVM for display
        //    var vm = new AttendanceDetailVM
        //    {
        //        AttID = attendance.Att,
        //        Name = attendance.Name,
        //        Date = attendance.Date,
        //        Records = attendance.Records
        //            .Select(r => new StudentAttendanceItemVM
        //            {
        //                UserID = r.UserID,
        //                StudentName = "",  // optional: fetch name from Users if needed
        //                Status = r.Status
        //            }).ToList()
        //    };

        //    return View(vm);
        //}
    }
}
