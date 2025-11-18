using Microsoft.EntityFrameworkCore;
using SIMS.Models;

namespace SIMS.Data
{
    public class SimsDbContext : DbContext
    {
        public SimsDbContext(DbContextOptions<SimsDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseClass> Classes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<CourseMaterial> CourseMaterials { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<ClassSchedule> ClassSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Lưu DayOfWeekEnum dưới dạng string
            modelBuilder.Entity<ClassSchedule>()
                .Property(e => e.DayOfWeek)
                .HasConversion<string>();

            // Lưu EnrollmentStatus dưới dạng string
            modelBuilder.Entity<Enrollment>()
                .Property(e => e.Status)
                .HasConversion<string>();

            // Lưu SubmissionStatus dưới dạng string
            modelBuilder.Entity<Submission>()
                .Property(e => e.Status)
                .HasConversion<string>();

            foreach (var fk in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict; // hoặc NoAction
            }
        }
    }
}
