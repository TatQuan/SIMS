using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMS.Models
{
    public class Submission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubmissionId { get; set; }

        [Required]
        public int AssignmentId { get; set; }
        [ForeignKey(nameof(AssignmentId))]
        public Assignment Assignment { get; set; }

        [Required]
        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }
        [Required]
        public string FileUrl { get; set; }
        public string Description { get; set; } = "No Description";
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
        [Column(TypeName = "decimal(5,2)")]
        public decimal? Grade { get; set; }
        public string? Feedback { get; set; }
        public int? GradedBy { get; set; }
        [ForeignKey(nameof(GradedBy))]
        public Faculty Faculty { get; set; }
        public DateTime? GradedAt { get; set; }

        public bool IsLate { get; set; } = false;

        public SubmissionStatus Status { get; set; } = SubmissionStatus.Draft;
    }
    public enum SubmissionStatus
    {
        Draft,      // chưa nộp
        Submitted,  // đã nộp
        Graded      // đã chấm
    }
}
