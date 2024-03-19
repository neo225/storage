global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Localization;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc.RazorPages;
global using Microsoft.EntityFrameworkCore;
global using System.Security.Claims;
global using System.Text.Json;
global using System.ComponentModel.DataAnnotations;
global using MR.AspNetCore.Pagination;
global using MR.EntityFrameworkCore.KeysetPagination;
global using IOL.Helpers;
global using Quality.Storage.Api.Database;
global using Quality.Storage.Api.Database.Models;
global using Quality.Storage.Api.Enums;
global using Quality.Storage.Api.Models;
global using Quality.Storage.Api.Statics;
global using Quality.Storage.Api.Services.Admin;
global using Quality.Storage.Api.Services.System;
global using Quality.Storage.Api.Utilities;
global using Quality.Storage.Api.Resources;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quality.Storage.Api.Utilities;

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
builder.Services.AddRequestLocalization(o => {
	o.DefaultRequestCulture = new RequestCulture("en");
	var cultures = new CultureInfo[] {
			new("en"),
			new("no")
	};
	o.SupportedCultures = cultures;
	o.SupportedUICultures = cultures;
});
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<StorageService>();
builder.Services.AddScoped<ShareService>();
builder.Services.AddScoped<DefaultResourceService>();
builder.Services.AddDbContext<AppDatabase>(o => {
	o.UseNpgsql(builder.Configuration.GetAppDbConnectionString(),
				b => {
					b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
					b.EnableRetryOnFailure(3);
				});
	o.UseSnakeCaseNamingConvention();
});
builder.Services.AddPagination();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddControllers();

var app = builder.Build();

app.UseStaticFiles();
app.UseStatusCodePages();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.UseRequestLocalization(app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value);
app.Run();
