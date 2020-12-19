using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DrivingSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var instructors = await _context.Instructors
                .Include(i => i.Image)
                .ToListAsync();
            
            return View(instructors);
        }

        [HttpGet]
        public async Task<IActionResult> GetTimetable()
        {
            var educationForms = await _context.EducationForms.ToListAsync();
            
            return View(educationForms);
        }
    }
}