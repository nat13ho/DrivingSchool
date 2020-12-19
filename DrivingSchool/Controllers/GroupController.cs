using System.Linq;
using System.Threading.Tasks;
using DrivingSchool.Models;
using DrivingSchool.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Controllers
{
    public class GroupController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(string sortString)
        {
            ViewData["DateSortParam"] = string.IsNullOrEmpty(sortString) ? "date_desc" : "";
            var groups = await _context.Groups
                .Include(g => g.EducationForm)
                .ToListAsync();
            var educationForms = await _context.EducationForms.ToListAsync();
            ViewBag.EducationForms = new SelectList(educationForms, "Id", "Name");

            groups = sortString switch
            {
                "date_desc" => groups.OrderByDescending(g => g.RecruitmentDate).ToList(),
                _ => groups.OrderBy(g => g.RecruitmentDate).ToList()
            };

            return View(new GroupIndexViewModel {Groups = groups});
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(GroupIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.EducationFormId))
                {
                    var educationForm = await _context.EducationForms
                        .FirstOrDefaultAsync(e => e.Id.Equals(model.EducationFormId));

                    if (educationForm != null)
                    {
                        var group = new Group()
                        {
                            RecruitmentDate = model.Date,
                            EducationForm = educationForm
                        };

                        await _context.AddAsync(group);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction("Index", "Group");
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Remove(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id.Equals(id));

                if (group != null)
                {
                    var trainingApplications =
                        await _context.TrainingApplications.Where(t => t.GroupId.Equals(group.Id)).ToListAsync();
                    
                    _context.TrainingApplications.RemoveRange(trainingApplications);
                    _context.Groups.Remove(group);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index", "Group");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var group = await _context.Groups
                    .Include(g => g.EducationForm)
                    .FirstOrDefaultAsync(g => g.Id.Equals(id));
                var educationForms = await _context.EducationForms.ToListAsync();
                ViewBag.EducationForms = new SelectList(educationForms, "Id", "Name");
                
                if (group != null)
                {
                    var model = new GroupEditViewModel() {Id = group.Id, Date = group.RecruitmentDate};
                    
                    return View(model);
                }
            }

            return RedirectToAction("Index", "Group");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(GroupEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var group = await _context.Groups
                    .Include(g => g.EducationForm)
                    .FirstOrDefaultAsync(g => g.Id.Equals(model.Id));
                var educationForm = await _context.EducationForms
                    .FirstOrDefaultAsync(e => e.Id.Equals(model.EducationFormId));

                if (group != null && educationForm != null)
                {
                    group.EducationForm = educationForm;
                    group.RecruitmentDate = model.Date;

                    await _context.SaveChangesAsync();
                }
            }
            
            return RedirectToAction("Index", "Group");
        }
    }
}