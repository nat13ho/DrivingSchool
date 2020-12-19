using System;

namespace DrivingSchool.Models
{
    public class TrainingApplication
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }

        public string GroupId { get; set; }
        public Group Group { get; set; }
    }
}