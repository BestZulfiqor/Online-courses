using System.ComponentModel.DataAnnotations;

namespace OnlineCoursesPlatform.Web.Models;

public class Module
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Order { get; set; }

    public int CourseId { get; set; }
    public Course? Course { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
