using Hotels.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Hotels.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//To link with Data File and SQL Server according to entities 
// builder.Services.AddDbContext<ApplicationDbContex>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//}
//);

builder.Services.AddDbContext<HotelsUserContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // To link with Identy File and SQL Server according to entities 
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{ 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // To link with Data File and SQL Server according to entities 
}); // then tools -> write in (package manager console) --- Add-Migration DirstMigration --- then --- update-database

builder.Services.AddDefaultIdentity<HotelsUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<HotelsUserContext>();
    //                                                                     anyname
builder.Services.AddRazorPages();
builder.Services.AddSession(); // config the session 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();  // to use the session 
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");
//pattern: "{controller=Shopping}/{action=Index}/{id?}");


app.Run();
