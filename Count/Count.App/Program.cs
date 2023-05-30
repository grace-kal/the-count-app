using Count.App.Mapper;
using Count.DataAccess;
using Count.DataAccess.Repositories;
using Count.DataAccess.Repositories.Interfaces;
using Count.DataAccess.Seeder;
using Count.Models;
using Count.Services;
using Count.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CountDbContext>(options =>
    options
    .UseSqlServer(connectionString)
    .UseLazyLoadingProxies());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CountDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddScoped<IMealRepo, MealRepo>();
builder.Services.AddScoped<IDayRepo, DayRepo>();
builder.Services.AddScoped<IFoodRepo, FoodRepo>();
builder.Services.AddScoped<IBmiRepo, BmiRepo>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IBmiService, BmiService>();
builder.Services.AddScoped<IDayService, DayService>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IFoodService, FoodService>();



var app = builder.Build();


using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<CountDbContext>();
    dbContext.Database.Migrate();
    new Seeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

    endpoints.MapAreaControllerRoute(
        name: "area", "Admin",
        pattern: "{area:exists }/{controller}/{action}/{id?}");
    endpoints.MapAreaControllerRoute(
       name: "area", "Agent",
       pattern: "{area:exists }/{controller}/{action}/{id?}");
});

app.Run();
