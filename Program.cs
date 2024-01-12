/*
Title:Application on LEAVE MANAGEMENT SYSTEM
Author:Nethrasree
Created on:05/03/2023
updated on:08/08/2023
Reviewed by:Sabapathi Shanmugam
Reviewed on:
*/

using LMS.Data;
using Microsoft.EntityFrameworkCore;
using LMS.Filter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var Default = builder.Configuration.GetConnectionString("Default");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(options=>
options.UseSqlServer(Default));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Login/adminlogin"; // Set the login URL
        });
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

 builder.Services.AddControllers(Options =>
 {
    Options.Filters.Add(new Filters());
   
 });
 builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
 builder.Services.AddDistributedMemoryCache();
 builder.Services.AddSession(options =>
 {
    options.IdleTimeout = TimeSpan.FromMinutes(15);
 });

 builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
     app.UseStatusCodePagesWithReExecute("/Error/{0}"); 
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
