using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Web.Data;
using OnlineCoursesPlatform.Web.Models;

namespace OnlineCoursesPlatform.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public List<Course> PopularCourses { get; set; } = new();
    public int TotalStudents { get; set; }
    public int TotalCourses { get; set; }
    public int TotalInstructors { get; set; }

    public async Task OnGetAsync()
    {
        // Get popular courses (for now, just get the latest 3 courses)
        PopularCourses = await _context.Courses
            .Include(c => c.Instructor)
            .OrderByDescending(c => c.CreatedAt)
            .Take(3)
            .ToListAsync();

        // Get statistics
        TotalStudents = await _userManager.GetUsersInRoleAsync("Student")
            .ContinueWith(t => t.Result.Count);

        TotalCourses = await _context.Courses.CountAsync();

        TotalInstructors = await _userManager.GetUsersInRoleAsync("Instructor")
            .ContinueWith(t => t.Result.Count);
    }
}
