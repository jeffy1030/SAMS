namespace SAMS.Models
{
    public class Attendance
    {
        public string? id { get; set; }
        public int AttID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<AttendanceRecord> Records { get; set; }
    }
}
    