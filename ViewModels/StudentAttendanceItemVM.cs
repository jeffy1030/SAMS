using System.Text.Json.Serialization;

namespace SAMS.ViewModels
{
    public class StudentAttendanceItemVM
    {
        [JsonPropertyName("User_ID")]  // keep it consistent with Users
        public int User_ID { get; set; }

        public string StudentName { get; set; } = "";

        public string Status { get; set; } = "Absent";
    }
}
