using System.Text.Json.Serialization;

namespace SAMS.Models
{
    public class AttendanceRecord
    {
        [JsonPropertyName("User_ID")]  // IMPORTANT: must match your Users.User_ID
        public int User_ID { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; } = "Absent";
    }
}
