using System;

namespace DrivingSchool.Models
{
    public class Instructor
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Fullname { get; set; }
        public double Experience { get; set; }

        public string ImageId { get; set; }
        public Image Image { get; set; }
    }
}