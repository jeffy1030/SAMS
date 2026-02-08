using System.Text.Json.Serialization;

namespace SAMS.Models
{
    public class Users
    {
        [JsonPropertyName("id")]
        public string? id { get; set; }

        [JsonPropertyName("User_ID")]
        public int User_ID { get; set; }

        [JsonPropertyName("Pass")]
        public string Pass { get; set; }

        [JsonPropertyName("FName")]
        public string FName { get; set; }

        [JsonPropertyName("MName")]
        public string? MName { get; set; }

        [JsonPropertyName("LName")]
        public string LName { get; set; }

        [JsonPropertyName("Role")]
        public string Role { get; set; }

        [JsonPropertyName("Gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("Email")]
        public string? Email { get; set; }

        [JsonPropertyName("IsActive")]
        public bool IsActive { get; set; }
    }

}
