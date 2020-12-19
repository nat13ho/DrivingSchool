using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DrivingSchool.Models;

namespace DrivingSchool.ViewModels
{
    public class GroupIndexViewModel
    {
        public List<Group> Groups { get; set; }
        
        [Required(ErrorMessage = "Укажите дату обучения!")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Укажите форму обучения!")]
        public string EducationFormId { get; set; }
    }
}