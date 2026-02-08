using Microsoft.AspNetCore.Mvc;
using SAMS.Models;
using System.Text;
using System.Text.Json;

namespace SAMS.Services
{
    public class MockApiService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions;

        public MockApiService(HttpClient http)
        {
            _http = http;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = {new UnixDateTimeConverter()}
            };
        }

        //USERS
        public async Task<List<Users>> GetUsersAsync()
        {
            var response = await _http.GetStringAsync("Users");
            return JsonSerializer.Deserialize<List<Users>>(response);
        }

        public async Task<List<Attendance>> GetAttendancesAsync()
        {
            var response = await _http.GetStringAsync("Attendance");
            return JsonSerializer.Deserialize<List<Attendance>>(response);
        }

        public async Task<Users?> LoginAsync(int userId, string password)
        {
            var users = await GetUsersAsync();
            return users.FirstOrDefault(u =>
                u.User_ID == userId &&
                u.Pass == password
            );
        }

        //TEACHERS
        public async Task<Users?> CreateTeacherAsync(Users newTeacher)
        {
            newTeacher.Role = "Teacher";
            newTeacher.IsActive = true;

            var users = await GetUsersAsync();

            // 2️⃣ Find max User_ID for teachers
            int maxId = 100000 - 1; // default starting point
            if (users != null && users.Any(u => u.Role == "Teacher"))
            {
                maxId = users
                    .Where(u => u.Role == "Teacher")
                    .Max(u => u.User_ID);
            }

            newTeacher.User_ID = maxId + 1;

            newTeacher.Pass = Convert.ToString(newTeacher.User_ID);

            var json = JsonSerializer.Serialize(newTeacher, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("Users", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Users>(result, _jsonOptions);
            }

            return null;
        }

        public async Task<Users?> CreateStudentAsync(Users student)
        {
            student.Role = "Student";
            student.IsActive = true;

            var users = await GetUsersAsync();

            // 2️⃣ Find max User_ID for students
            int maxId = 10000 - 1; // default starting point
            if (users != null && users.Any(u => u.Role == "Student"))
            {
                maxId = users
                    .Where(u => u.Role == "Student")
                    .Max(u => u.User_ID);
            }


            // 3️⃣ Assign next ID
            student.User_ID = maxId + 1;

            student.Pass = Convert.ToString(student.User_ID);

            var json = JsonSerializer.Serialize(student, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("Users", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Users>(result, _jsonOptions);
            }

            return null;
        }

        //public async Task<Users?> DeleteTeacher(Users teacher)
        //{
        //    teacher.
        //    return null;
        //}
    }
}
