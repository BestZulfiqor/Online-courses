using System.ComponentModel.DataAnnotations;

namespace OnlineCoursesPlatform.Web.Models;

public class Lesson
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string VideoUrl { get; set; } = string.Empty;

    public int Order { get; set; }

    public int ModuleId { get; set; }
    public Module? Module { get; set; }
}
