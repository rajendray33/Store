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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Specify the path to redirect users who need to sign in
        options.LoginPath = "/Login/signin";

        // Specify the path to redirect users who don't have access
        options.AccessDeniedPath = "/Account/AccessDenied";

        // Set the expiration time of the authentication cookie
        options.ExpireTimeSpan = TimeSpan.FromDays(15); // Cookie expiration time

        // Optionally, enable sliding expiration
        options.SlidingExpiration = true;

        // Optionally, configure the cookie name
        options.Cookie.Name = "YourAppCookieName";

        // Optionally, configure other cookie settings
        options.Cookie.HttpOnly = true; // Make the cookie accessible only via HTTP(S)
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Use the cookie only over HTTPS (same as request)
        options.Cookie.SameSite = SameSiteMode.Lax; // Configure SameSite attribute
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
