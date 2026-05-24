using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public class ContosoUniversityContext : DbContext
    {
        public ContosoUniversityContext (DbContextOptions<ContosoUniversityContext> options)
            : base(options)
        {
        }

        public DbSet<ContosoUniversity.Models.Student> Students { get; set; } = default!;
        public DbSet<ContosoUniversity.Models.Course> Courses { get; set; } = default!;
        public DbSet<ContosoUniversity.Models.Enrollment> Enrollments { get; set; } = default!;
        public DbSet<ContosoUniversity.Models.Department> Departments { get; set; }
        public DbSet<ContosoUniversity.Models.Instructor> Instructors { get; set; }
        public DbSet<ContosoUniversity.Models.OfficeAssignment> OfficeAssignments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Этот код имеет смысл только в том случае, когда имя DbSet<>-a, обновленного выше,
            //отличается от имени таблиыц в Базе.
            //Если же имя каждого DbSet<> в точности совпадает с именем соответсвующей таблицы в Базе,
            // этот метод даже не нужно переопределять.
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Instructor>().ToTable("Instructors");
            modelBuilder.Entity<Course>().ToTable("Courses")
                .HasMany(c=> c.Instructors)
                .WithMany(i => i.Courses)
        }
    }
}
