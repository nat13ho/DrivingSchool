using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Введите Email!")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Введите пароль!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}