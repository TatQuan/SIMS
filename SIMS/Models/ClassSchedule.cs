using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMS.Models
{
    public class ClassSchedule
    {
        [Key]
        public int ScheduleId { get; set; }
        [Required]
        public int ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public CourseClass Class { get; set; }
        [Required]
        public DayOfWeekEnum DayOfWeek { get; set; }
        public DateTime ScheduleDate { get; set; } = DateTime.Now;
        [Required]
        public TimeOnly StartTime { get; set; } 
        [Required]
        public TimeOnly EndTime { get; set; }
        [Required]
        public string Room { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public enum DayOfWeekEnum
    {
        Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
    }
}
