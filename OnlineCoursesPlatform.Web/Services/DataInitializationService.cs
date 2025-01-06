using Microsoft.AspNetCore.Identity;
using OnlineCoursesPlatform.Web.Data;
using OnlineCoursesPlatform.Web.Models;

namespace OnlineCoursesPlatform.Web.Services;

public class DataInitializationService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DataInitializationService(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitializeAsync()
    {
        // Create roles if they don't exist
        string[] roles = { "Admin", "Instructor", "Student" };
        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Create admin user if it doesn't exist
        var adminEmail = "admin@example.com";
        var adminUser = await _userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true,
                IsInstructor = true
            };
            await _userManager.CreateAsync(adminUser, "Admin123!");
            await _userManager.AddToRoleAsync(adminUser, "Admin");
            await _userManager.AddToRoleAsync(adminUser, "Instructor");
        }

        // Create instructor if it doesn't exist
        var instructorEmail = "instructor@example.com";
        var instructor = await _userManager.FindByEmailAsync(instructorEmail);
        if (instructor == null)
        {
            instructor = new ApplicationUser
            {
                UserName = instructorEmail,
                Email = instructorEmail,
                FirstName = "John",
                LastName = "Doe",
                EmailConfirmed = true,
                IsInstructor = true
            };
            await _userManager.CreateAsync(instructor, "Instructor123!");
            await _userManager.AddToRoleAsync(instructor, "Instructor");
        }

        // Add demo courses if they don't exist
        if (!_context.Courses.Any())
        {
            var courses = new List<Course>
            {
                new Course
                {
                    Title = "Веб-разработка с ASP.NET Core",
                    Description = "Изучите основы веб-разработки с использованием ASP.NET Core. В этом курсе вы научитесь создавать современные веб-приложения, используя последние технологии от Microsoft.",
                    ImageUrl = "https://placehold.co/600x400?text=ASP.NET+Core",
                    Price = 599.99M,
                    InstructorId = instructor.Id,
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Title = "JavaScript для начинающих",
                    Description = "Полное руководство по JavaScript для начинающих. Изучите основы программирования, работу с DOM, асинхронное программирование и многое другое.",
                    ImageUrl = "https://placehold.co/600x400?text=JavaScript",
                    Price = 499.99M,
                    InstructorId = instructor.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                },
                new Course
                {
                    Title = "Python: от новичка до профессионала",
                    Description = "Comprehensive курс по Python, охватывающий все аспекты языка: от базового синтаксиса до продвинутых тем, включая работу с базами данных и веб-фреймворки.",
                    ImageUrl = "https://placehold.co/600x400?text=Python",
                    Price = 699.99M,
                    InstructorId = adminUser.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                }
            };

            _context.Courses.AddRange(courses);
            await _context.SaveChangesAsync();

            // Add modules and lessons for the first course
            var webDevCourse = courses[0];
            var modules = new List<Module>
            {
                new Module
                {
                    Title = "Введение в ASP.NET Core",
                    Description = "Основы фреймворка ASP.NET Core",
                    Order = 1,
                    CourseId = webDevCourse.Id,
                    Lessons = new List<Lesson>
                    {
                        new Lesson
                        {
                            Title = "Что такое ASP.NET Core",
                            Content = "Обзор фреймворка и его преимущества",
                            VideoUrl = "https://example.com/video1",
                            Order = 1
                        },
                        new Lesson
                        {
                            Title = "Настройка среды разработки",
                            Content = "Установка необходимого ПО",
                            VideoUrl = "https://example.com/video2",
                            Order = 2
                        }
                    }
                },
                new Module
                {
                    Title = "Работа с базами данных",
                    Description = "Entity Framework Core и CRUD операции",
                    Order = 2,
                    CourseId = webDevCourse.Id,
                    Lessons = new List<Lesson>
                    {
                        new Lesson
                        {
                            Title = "Entity Framework Core",
                            Content = "Основы работы с EF Core",
                            VideoUrl = "https://example.com/video3",
                            Order = 1
                        },
                        new Lesson
                        {
                            Title = "CRUD операции",
                            Content = "Создание, чтение, обновление и удаление данных",
                            VideoUrl = "https://example.com/video4",
                            Order = 2
                        }
                    }
                }
            };

            _context.Modules.AddRange(modules);
            await _context.SaveChangesAsync();
        }
    }
}
