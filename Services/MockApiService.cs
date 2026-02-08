using Microsoft.AspNetCore.Mvc;
using SAMS.Models;
using SAMS.ViewModels;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public async Task<List<Users>> GetTeachersAsync()
        {
            var users = await GetUsersAsync();
            return users
                .Where(u => u.Role == "Teacher")
                .ToList();
        }

        public async Task<List<Users>> GetStudentAsync()
        {
            var users = await GetUsersAsync();
            return users
                .Where(u => u.Role == "Student")
                .ToList();
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

        public async Task<Users?> GetTeacherByIdAsync(string id)
        {
            var users = await GetUsersAsync(); // existing method
            return users.FirstOrDefault(u => u.id == id && u.Role == "Teacher");
        }

        public async Task<bool> UpdateTeacherAsync(Users updatedTeacher)
        {
            if (updatedTeacher == null)
            {
                Console.WriteLine("UpdateTeacherAsync: updatedTeacher is null");
                return false;
            }

            try
            {
                Console.WriteLine($"Updating teacher ID: {updatedTeacher.id}");

                // Serialize with default options (uses JsonPropertyName attributes)
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null, // preserve the JsonPropertyName
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                string payload = JsonSerializer.Serialize(updatedTeacher, jsonOptions);
                Console.WriteLine($"Payload being sent:\n{payload}");

                // Send PUT request to MockAPI
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await _http.PutAsync($"Users/{updatedTeacher.id}", content);

                Console.WriteLine($"Response status: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response: {errorContent}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateTeacherAsync failed: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return false;
            }
        }


        public async Task<Users?> GetStudentByIdAsync(int id)
        {
            var users = await GetUsersAsync(); // existing method
            return users.FirstOrDefault(u => u.User_ID == id && u.Role == "Student");
        }

        //public async Task<Users?> DeleteTeacher(Users teacher)
        //{
        //    teacher.
        //    return null;
        //}
    }
}
