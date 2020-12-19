using System;
using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.ViewModels
{
    public class GroupEditViewModel
    {
        [Required]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string EducationFormId { get; set; }
    }
}