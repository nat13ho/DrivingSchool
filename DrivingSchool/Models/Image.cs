using System;

namespace DrivingSchool.Models
{
    public class Image
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Path { get; set; }
    }
}