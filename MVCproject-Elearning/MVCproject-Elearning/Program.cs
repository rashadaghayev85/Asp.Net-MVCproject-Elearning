using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCproject_Elearning.Data;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services;
using MVCproject_Elearning.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<AppUser, IdentityRole>()
                                   .AddEntityFrameworkStores<AppDBContext>()
                                   .AddDefaultTokenProviders();

builder.Services.AddDbContext<AppDBContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IInformationService, InformationService>();
builder.Services.AddScoped<IIconService, IconService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<ISocialService, SocialService>();
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=dashboard}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
