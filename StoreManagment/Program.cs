using Microsoft.EntityFrameworkCore;
using StoreManagment.Models;
using StoreManagment.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the DbContext with dependency injection
builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Dbcs")));

// Configure authentication and authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/signin"; // Set your login path
        options.AccessDeniedPath = "/Account/AccessDenied"; // Set your access denied path if needed
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
});

// Register repositories with dependency injection
builder.Services.AddScoped<LoginRepository>();

// Add session services
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization(); // Add authorization middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
