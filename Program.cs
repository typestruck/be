using System.Threading.Channels;
using be.Components;
using be.Data;
using be.Endpoints;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var channel = Channel.CreateUnbounded<GameEvent>();

builder.Services.AddSingleton(channel.Reader);
builder.Services.AddDbContextFactory<BeDataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("BeDataContext")));
builder.Services.AddRazorComponents();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.Name = "becookie";
    options.ExpireTimeSpan = new TimeSpan(10000, 1, 1, 1);
});
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorComponents<App>();

app.MapPost("user/create", User.Create);
app.MapGet("game/events", Game.Events);

app.Run();