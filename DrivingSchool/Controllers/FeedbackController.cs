using System.Threading.Tasks;
using DrivingSchool.Models;
using DrivingSchool.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbackController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var feedbacks = await _context.Feedbacks.ToListAsync();
            
            return View(new FeedbackIndexViewModel {Feedbacks = feedbacks});
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(FeedbackIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                var feedback = new Feedback()
                {
                    Fullname = model.Fullname,
                    Email = model.Email,
                    Content = model.Content
                };

                await _context.AddAsync(feedback);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Feedback");
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Remove(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var feedback = await _context.Feedbacks.FirstOrDefaultAsync(g => g.Id.Equals(id));

                if (feedback != null)
                {
                    _context.Feedbacks.Remove(feedback);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index", "Feedback");
        }
    }
}