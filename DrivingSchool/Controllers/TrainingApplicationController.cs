using System.Linq;
using System.Threading.Tasks;
using DrivingSchool.Models;
using DrivingSchool.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Controllers
{
    public class TrainingApplicationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingApplicationController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult SignUp(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var model = new SignUpViewModel {GroupId = id};
                return View(model);
            }
            
            return RedirectToAction("Index", "Group");
        }
        
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var group = await _context.Groups
                    .Include(g => g.EducationForm)
                    .FirstOrDefaultAsync(g => g.Id.Equals(model.GroupId));

                if (group != null)
                {
                    var trainingApplication = new TrainingApplication
                    {
                        Fullname = model.Fullname,
                        Email = model.Email,
                        Phone = model.Phone,
                        Message = model.Message,
                        Group = group
                    };

                    await _context.TrainingApplications.AddAsync(trainingApplication);
                    await _context.SaveChangesAsync();
                }
            }
            
            return RedirectToAction("Index", "Group");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var application = await _context.TrainingApplications
                        .FirstOrDefaultAsync(t => t.Id.Equals(id));

                if (application != null)
                {
                    _context.TrainingApplications.Remove(application);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("GetList", "TrainingApplication");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var trainingApplications = await _context.TrainingApplications
                .Include(t => t.Group)
                .Include(t => t.Group.EducationForm)
                .OrderBy(t => t.Group)
                .ToListAsync();

            return View(trainingApplications);
        }
    }
}