using System.Text.Json.Serialization;

namespace SAMS.Models
{
    public class Attendance
    {
        [JsonPropertyName("id")]
        public string? id { get; set; }

        [JsonPropertyName("AttID")]
        public int AttID { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("Records")]
        public List<AttendanceRecord> Records { get; set; }
    }
}
    