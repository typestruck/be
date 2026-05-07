using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace be.Services;

public class CurrentlyAuthenticated : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity(
        [
            new Claim("Id", "100"),
        ], "Custom Authentication");

        var user = new ClaimsPrincipal(identity);

        return Task.FromResult(new AuthenticationState(user));
    }
}