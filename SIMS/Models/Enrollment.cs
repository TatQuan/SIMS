using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace SIMS.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }
        [Required]
        public int ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public CourseClass Class { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.Now;

        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Studying;
    }

    public enum EnrollmentStatus
    {
        Studying,
        Completed,
        Dropped
    }
}
