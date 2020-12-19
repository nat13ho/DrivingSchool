using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchool.Models
{
    public class EducationForm
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Duration { get; set; }
        
        public List<DayOfWeek> TrainingDays { get; set; }
    }
}