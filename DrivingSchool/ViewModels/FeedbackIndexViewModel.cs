using System.Collections.Generic;
using DrivingSchool.Models;

namespace DrivingSchool.ViewModels
{
    public class FeedbackIndexViewModel
    {
        public List<Feedback> Feedbacks { get; set; }
        public string Fullname{ get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
    }
}