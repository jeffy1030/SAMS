namespace SAMS.Models
{
    public class Attendance
    {
        public int AttID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<AttendanceRecord> Records { get; set; }
    }
}
