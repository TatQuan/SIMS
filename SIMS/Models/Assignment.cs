using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMS.Models
{
    public class Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignmentId { get; set; }
        [Required]
        public int ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public CourseClass Class { get; set; }
        public string Title { get; set; } = "no Title";
        public string Description { get; set; } = "No Description";
        public string? FileUrl { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public int FacultyId { get; set; }
        [ForeignKey(nameof(FacultyId))]
        public Faculty Faculty { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
