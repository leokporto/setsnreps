using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using SetsnReps.API.Entities;
using SetsnReps.API.Services.Identity;

namespace SetsnReps.API.Endpoints.Auth;

public static class AuthExtensions
{
    public static WebApplication UseAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/auth")
            .WithTags("Authentication");
        
        JwtService jwtService = app.Services.GetRequiredService<JwtService>();
        
        group.MapPost("/register", async (RegisterRequest request, UserManager<AppUser> userManager) =>
        {
            var user = new AppUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return Results.BadRequest(result.Errors);

            return Results.Ok("User created");
        });

        group.MapPost("/login", async (
            LoginRequest request,
            UserManager<AppUser> userManager,
            IConfiguration config) =>
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
                return Results.Unauthorized();

            var token = jwtService.GenerateToken(user);

            return Results.Ok(new { token });
        });
        
        group.MapGet("/me", [Authorize] async (ClaimsPrincipal userPrincipal, UserManager<AppUser> userManager) =>
        {
            var userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await userManager.FindByIdAsync(userId!);

            if (user == null)
                return Results.NotFound();

            return Results.Ok(new
            {
                user.Id,
                user.UserName,
                user.Email
            });
        });
        
        return app;
    }
    
    
}