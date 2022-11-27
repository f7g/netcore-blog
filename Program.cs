// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/?view=aspnetcore-7.0&tabs=macos
using Blog.Data;
using Blog.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

// Add services to the container
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BlogDbContext>(options => {
    options.UseSqlServer(connectionString);
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<BlogDbContext>();
builder.Services.ConfigureApplicationCookie(options => { // Overwrite the redirect login url
    options.LoginPath = "/Auth/Login";
});
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IRepository, Repository>();

/*
 * Configure the HTTP request pipeline
 * NOTE: .GetAwaiter().GetResult() allows you to run an async function
*/
var app = builder.Build();
try {
    var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    context.Database.EnsureCreated();

    // If there are no roles, then create the first role
    if (!context.Roles.Any()) {
        var adminRole = new IdentityRole("Admin");
        roleManager.CreateAsync(adminRole).GetAwaiter().GetResult(); 
    }
    // If there are no admin users, then create the first admin user (admin)
    if (!context.Users.Any(u => u.UserName == "admin")) {
        var adminUser = new IdentityUser { UserName = "admin", Email = "admin@admin.com" };
        userManager.CreateAsync(adminUser, "Password123!").GetAwaiter().GetResult();
        userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
    }
} catch (Exception e) {
    Console.WriteLine("Unable to initiate user and role", e.Message);
}

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.MapRazorPages();

// Start the application
app.Run();
