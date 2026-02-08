namespace SAMS.Models
{
    public class Users
    {
        public string? id { get; set; }
        public int User_ID { get; set; }
        public string Pass { get; set; }
        public string FName { get; set; }
        public string? MName { get; set; }
        public string LName { get; set; }
        public string Role { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }  
        public bool IsActive { get; set; }
    }
}
