using Gym.API;
using Microsoft.EntityFrameworkCore;

namespace Gym.API.Endpoints;

public static class ExerciseEndpoints
{
    public static void MapExerciseEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/exercises");

        group.MapGet("/",async (GymContext dbContext)=>await dbContext.execises);
    }
}