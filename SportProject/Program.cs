using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.EntityFrameworkCore;
using SportProject.Data;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using SportProject.Services;

var builder = WebApplication.CreateBuilder(args);

// 이거 추가
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LeaderDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("AthleticLeaderDB");
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<ILeaderService, LeaderService>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAll"); // 이거 추가

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Start}/{id?}");

app.Run();
