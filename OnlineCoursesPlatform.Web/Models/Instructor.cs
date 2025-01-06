using System.ComponentModel.DataAnnotations;

namespace OnlineCoursesPlatform.Web.Models;

public class Instructor
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public string Bio { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string ProfilePictureUrl { get; set; } = string.Empty;

    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
