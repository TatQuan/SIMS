using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMS.Models
{
    public class CourseMaterial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
        [Required]
        public int ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public CourseClass Class { get; set; }
        public string? Title { get; set; } = "No Title";
        public string? Description { get; set; } = "No Description";
        public string MaterialType { get; set; } = "General";
        public string? CloudUrl { get; set; }
        [Required]
        public int FacultyId { get; set; }
        [ForeignKey(nameof(FacultyId))]
        public Faculty Faculty { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
