using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DrivingSchool.Models;
using Microsoft.AspNetCore.Http;

namespace DrivingSchool.ViewModels
{
    public class InstructorIndexViewModel
    {
        public List<Instructor> Instructors { get; set; }
        [Required(ErrorMessage = "Введите ФИО инструктора!")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Введите опыт преподавания!")]
        public double Experience { get; set; }
        public IFormFile Image { get; set; }
    }
}