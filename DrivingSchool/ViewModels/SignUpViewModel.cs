using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        public string GroupId { get; set; }
        
        [Required(ErrorMessage = "Введите ФИО!")]
        public string Fullname { get; set; }
        
        [EmailAddress]
        [Required(ErrorMessage = "Введите адрес электронной почты!")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Введите номер телефона!")]
        public string Phone { get; set; }

        public string Message { get; set; }
    }
}