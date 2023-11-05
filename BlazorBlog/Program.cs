using Blazor.Blog.Data;
using Blazor.Blog.Models;
using Radzen;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeAreaFolder("Account", "/");
    options.Conventions.AllowAnonymousToAreaPage("Account","/Login");
    options.Conventions.AllowAnonymousToAreaPage("Account","/Logout");
    options.Conventions.AllowAnonymousToAreaPage("Account", "/Create");
    options.Conventions.AddPageRoute("/Sitemap", "Sitemap.xml");
});
builder.Services.AddServerSideBlazor();
var cs = builder.Configuration.GetConnectionString("default");
builder.Services.AddTransient<IBlogPostService, BlogPostServiceEF>();
builder.Services.AddTransient<IBlogCategoryService, BlogCategoryServiceEF>();
builder.Services.AddTransient<IAuthorService, AuthorServiceEF>();
builder.Services.AddTransient<IImageUploadService, ImageUploadService>();
builder.Services.AddDbContextFactory<DataContext>(options =>
    options.UseSqlServer(cs));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<DataContext>();
builder.Services.AddControllers();
builder.Services.AddRadzenComponents();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<DialogService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
// import order of middleware is VERY important
app.UseAuthentication();
app.UseAuthorization();
app.MapBlazorHub(options =>
{
    options.WebSockets.CloseTimeout = new TimeSpan(1, 1, 1);
    options.CloseOnAuthenticationExpiration = true;
});
app.MapFallbackToPage("/_Host");
app.MapControllers();
app.Run();

