global using Microsoft.AspNetCore.Mvc;
global using IOL.Helpers;
global using I2R.Storage.Api.Services.Admin;
global using Microsoft.AspNetCore.Mvc.RazorPages;
global using Microsoft.EntityFrameworkCore;
global using I2R.Storage.Api.Database;
global using I2R.Storage.Api.Utilities;
global using I2R.Storage.Api.Database.Models;
global using I2R.Storage.Api.Resources;
global using I2R.Storage.Api.Enums;
global using Microsoft.Extensions.Localization;
global using I2R.Storage.Api.Statics;
global using Microsoft.AspNetCore.Authorization;
global using System.Security.Claims;
global using I2R.Storage.Api.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o => {
        o.Cookie.Name = "storage_session";
        o.Cookie.HttpOnly = true;
        o.SlidingExpiration = true;
        o.Events.OnRedirectToAccessDenied =
            o.Events.OnRedirectToLogin = c => {
                c.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.FromResult<object>(null);
            };
    });
builder.Services.AddLocalization();
builder.Services.AddRequestLocalization(o => { o.DefaultRequestCulture = new RequestCulture("en"); });
builder.Services.AddScoped<UserService>();
builder.Services.AddDbContext<AppDatabase>(o => {
    o.UseNpgsql(builder.Configuration.GetAppDbConnectionString(), b => {
        b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        b.EnableRetryOnFailure(3);
    });
    o.UseSnakeCaseNamingConvention();
});
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddControllers();

var app = builder.Build();

app.UseStaticFiles();
app.UseStatusCodePages();
app.UseRequestLocalization();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.Run();