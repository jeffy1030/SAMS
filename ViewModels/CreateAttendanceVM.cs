namespace SAMS.ViewModels
{
    public class CreateAttendanceVM
    {
        public string Name { get; set; } = "";

        public DateTime Date { get; set; }

        public List<StudentAttendanceItemVM> Students { get; set; } = new List<StudentAttendanceItemVM>();
    }
}

