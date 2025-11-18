using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SIMS.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }  
        [Required]
        public string Username { get; set; }  
        [Required]
        public string Password { get; set; }  
        [Required]
        public string FullName { get; set; } 
        [Required]
        public string Role { get; set; }  
        public string? Email { get; set; }  // Nullable cho Email
        public string? Address { get; set; }  
        public string? Gender { get; set; }  
        public string? DateOfBirth { get; set; }  
        public DateTime CreatedAt { get; set; } = DateTime.Now;  // Mặc định là thời gian hiện tại khi tạo user
        [Required]
        public bool IsDeleted { get; set; } = false;  // Mặc định là false, đánh dấu chưa xóa
    }
}
