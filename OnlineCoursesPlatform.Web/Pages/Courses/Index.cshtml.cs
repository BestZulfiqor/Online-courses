using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCoursesPlatform.Web.Models;
using OnlineCoursesPlatform.Web.Services;

namespace OnlineCoursesPlatform.Web.Pages.Courses;

public class IndexModel : PageModel
{
    private readonly ICourseService _courseService;

    public IndexModel(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public List<Course> Courses { get; set; } = new();

    public async Task OnGetAsync()
    {
        Courses = await _courseService.GetAllCoursesAsync();
    }
}
