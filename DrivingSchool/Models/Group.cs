using System;

namespace DrivingSchool.Models
{
    public class Group
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime RecruitmentDate { get; set; }
        
        public string EducationFormId { get; set; }
        public EducationForm EducationForm { get; set; }
    }
}