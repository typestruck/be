using be.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using be.Data;
using be.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BeDataContext") ?? throw new InvalidOperationException("Connection string 'BeDataContext' not found.");

builder.Services.AddDbContextFactory<BeDataContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddRazorComponents();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie("cookie", options =>
{
    options.Cookie.Name = "becookie";
    options.ExpireTimeSpan = new TimeSpan(10000, 1, 1, 1);
});
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CurrentlyAuthenticated>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorComponents<App>();

app.Run();