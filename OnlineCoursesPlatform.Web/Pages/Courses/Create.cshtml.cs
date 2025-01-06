using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCoursesPlatform.Web.Models;
using OnlineCoursesPlatform.Web.Services;

namespace OnlineCoursesPlatform.Web.Pages.Courses;

[Authorize(Roles = "Admin,Instructor")]
public class CreateModel : PageModel
{
    private readonly ICourseService _courseService;
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateModel(ICourseService courseService, UserManager<ApplicationUser> userManager)
    {
        _courseService = courseService;
        _userManager = userManager;
    }

    [BindProperty]
    public Course Course { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        Course.InstructorId = user.Id;
        Course.CreatedAt = DateTime.UtcNow;

        await _courseService.CreateCourseAsync(Course);
        return RedirectToPage("./Index");
    }
}
