using System;
using System.Collections.Generic;
using System.Linq;
using DrivingSchool.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace DrivingSchool
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<TrainingApplication> TrainingApplications { get; set; }
        public DbSet<EducationForm> EducationForms { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<EducationForm>()
                .Property(e => e.TrainingDays)
                .HasConversion(v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<DayOfWeek>>(v));
            base.OnModelCreating(builder);
        }
    }
}