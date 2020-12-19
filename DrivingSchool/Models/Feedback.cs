using System;
using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.Models
{
    public class Feedback
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required(ErrorMessage = "Отзыв не может быть пустым!")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Введите ФИО!")]
        public string Fullname { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Введите электронную почту!")]
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}