using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCoursesPlatform.Web.Models;

public class Enrollment
{
    public int Id { get; set; }

    [Required]
    public string StudentId { get; set; } = string.Empty;

    [ForeignKey("StudentId")]
    public ApplicationUser? Student { get; set; }

    public int CourseId { get; set; }
    
    [ForeignKey("CourseId")]
    public Course? Course { get; set; }

    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
}
