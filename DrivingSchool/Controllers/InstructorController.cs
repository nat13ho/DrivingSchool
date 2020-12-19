using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DrivingSchool.Models;
using DrivingSchool.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        
        public InstructorController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            ViewData["CurrentFilter"] = search;
            var instructors = await _context.Instructors
                .Include(i => i.Image)
                .ToListAsync();
            
            if (!string.IsNullOrEmpty(search))
            {
                instructors = instructors.Where(i => i.Fullname.ToLower().Contains(search.ToLower())).ToList();
            }

            return View(new InstructorIndexViewModel {Instructors = instructors});
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(InstructorIndexViewModel model)
        {
            if (_context.Instructors.Select(i => i.Fullname.ToLower()).Contains(model.Fullname.ToLower())) 
            {
                ModelState.AddModelError(string.Empty, "Такой инструктор уже существует");    
            }
            
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    var imageNames = _context.Images.Select(i => i.Name).ToList();
                    Image image = null;
                        
                    if (imageNames.Contains(model.Image.FileName))
                    {
                        image =
                            await _context.Images.FirstOrDefaultAsync(i => i.Name == model.Image.FileName);
                    }
                    else
                    {
                        var path = "/images/" + model.Image.FileName;

                        await using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                        {
                            await model.Image.CopyToAsync(fileStream);
                        }

                        await _context.Images.AddAsync(new Image {Name = model.Image.FileName, Path = path});
                        await _context.SaveChangesAsync();
                        image = await _context.Images.FirstOrDefaultAsync(i => i.Path == path);
                    }

                    if (image != null)
                    {
                        var instructor = new Instructor
                        {
                            Fullname = model.Fullname,
                            Experience = model.Experience,
                            Image = image
                        };
                            
                        await _context.Instructors.AddAsync(instructor);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Отсутствует изображение");
                }
            }
            
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Remove(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var instructor = await _context.Instructors
                    .Include(i => i.Image)
                    .FirstOrDefaultAsync(i => i.Id.Equals(id));

                if (instructor != null)
                {
                    var image = await _context.Images.FirstOrDefaultAsync(i => i.Id.Equals(instructor.ImageId));

                    if (image != null)
                    {
                        var imageCount = _context.Instructors.Count(i => i.ImageId.Equals(image.Id));

                        if (imageCount == 1)
                        {
                            _context.Images.Remove(image);
                        }
                        
                        _context.Instructors.Remove(instructor);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}