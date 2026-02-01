using System.ComponentModel.DataAnnotations;

namespace SAMS.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public int User_ID { get; set; }
        [Required]
        public string Pass { get; set; }
    }
}
