using System.Text.Json.Serialization;

namespace SAMS.Models
{
    public class AttendanceRecord
    {
        [JsonPropertyName("UserID")]
        public int UserID { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; }
    }
}
