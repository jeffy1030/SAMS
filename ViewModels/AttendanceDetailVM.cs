using System.Text.Json.Serialization;

namespace SAMS.ViewModels
{
    public class AttendanceDetailVM
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        public List<StudentAttendanceItemVM> Records { get; set; }
            = new List<StudentAttendanceItemVM>();
    }
}
