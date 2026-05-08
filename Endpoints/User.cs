using System.Security.Claims;
using be.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace be.Endpoints;

public static class User
{
    public static async Task<IResult> Create(Models.User? user, HttpContext http, BeDataContext db)
    {
        if (user == null || user?.Id == 0)
        {
            var saved = new Models.User { Name = "Guest Player" };

            db.User.Add(saved);
            await db.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new("Id", saved.Id.ToString()),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await http.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Results.Json(saved);
        }

        return Results.Json(user);
    }
}