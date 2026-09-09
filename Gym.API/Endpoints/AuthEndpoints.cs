using Gym.API.Dtos;
using Gym.API.Services;

namespace Gym.API.Endpoints;

public static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/auth");

        group.MapPost("/register", async (RegisterDto registerDto, UserService userService) =>
    {
        var user = await userService.RegisterAsync(registerDto);
        if (user is null)
        {
            return Results.Conflict(new
            {
                message = "A user with this email already exists."
            });
        }
        return Results.Ok(new
        {
            user.Id,
            user.Email
        });
    });
        return group;

    }
}