using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrivingSchool.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool
{
    public class DatabaseInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await context.EducationForms.AnyAsync())
            {
                var dateNow = DateTime.Now;
                
                var educationForms = new List<EducationForm>
                {
                    new EducationForm()
                    {
                        Name = "Утренняя", 
                        Price = 630m, 
                        Duration = 3.5, 
                        StartTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 8, 0, 0),
                        EndTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 12, 0, 0),
                        TrainingDays = new List<DayOfWeek>
                        {
                            DayOfWeek.Monday,
                            DayOfWeek.Wednesday
                        }
                    },
                    new EducationForm()
                    {
                        Name = "Вечерняя", 
                        Price = 650m, 
                        Duration = 3.5, 
                        StartTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 18, 0, 0),
                        EndTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 22, 0, 0),
                        TrainingDays = new List<DayOfWeek>
                        {
                            DayOfWeek.Tuesday,
                            DayOfWeek.Thursday
                        }
                    },
                    new EducationForm()
                    {
                        Name = "Выходного дня", 
                        Price = 650m, 
                        Duration = 3.5, 
                        StartTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 10, 0, 0),
                        EndTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 14, 0, 0),
                        TrainingDays = new List<DayOfWeek>
                        {
                            DayOfWeek.Saturday,
                            DayOfWeek.Sunday
                        }
                    }
                };
                
                await context.AddRangeAsync(educationForms);
            }
            
            if (!await userManager.Users.AnyAsync())
            {
                var user = new User() {Email = "danilagalynya@gmail.com", UserName = "danilagalynya@gmail.com"};
                var adminRole = await roleManager.FindByNameAsync("Admin");
                var result = await userManager.CreateAsync(user, "password");
                
                if (result.Succeeded && adminRole != null)
                {
                    await userManager.AddToRoleAsync(user, adminRole.Name);
                }
            }
        }
    }
}