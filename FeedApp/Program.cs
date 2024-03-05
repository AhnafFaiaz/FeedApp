using Microsoft.EntityFrameworkCore;
using FeedApp.Infra.Data;
using FeedApp.UI.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PrimaryDbConnection"));
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Signin/Signin"; // Set the login page
                /*options.AccessDeniedPath = "/Signin/Signin";*/ /// Set the access denied page
            });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("Admin"); // Users must have the "Admin" role
    });
    options.AddPolicy("UserPolicy", policy =>
    {
        policy.RequireRole("User");
    });
});


builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.RegisterService();
builder.Services.AddHttpContextAccessor();
Email.DefaultSender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
{
    Port = 587,
    Credentials = new NetworkCredential("systemfeedapp@gmail.com", "System-FeedApp21"),
    EnableSsl = true,
    

});




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.Use(async (context, next) =>
{
    context.Response.OnStarting(state =>
    {
        var httpContext = (HttpContext)state;
        httpContext.Response.Headers["Cache-Control"] = "no-cache, no-store";
        httpContext.Response.Headers["Pragma"] = "no-cache";
        httpContext.Response.Headers["Expires"] = "0";
        return Task.CompletedTask;
    }, context);

    await next();
});
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    //endpoints.MapDefaultControllerRoute();
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Signin}/{action=Signin}/{Id?}");
});


//app.MapRazorPages();

app.Run();

