using Gym.API.Dtos;
using Gym.API.Models;
using Gym.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Gym.API.Endpoints;

public static class WorkoutEndpoints
{
    public static void MapExerciseEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/workouts");
        group.MapGet("/", async (WorkoutService workoutService) =>
        {
            var workouts = await workoutService.GetAllWorkoutsAsync();

            return Results.Ok(workouts);
        });

        group.MapPost("/", async (CreateWorkoutDto newWorkout, WorkoutService workoutService) =>
        {
            var workout = await workoutService.CreateWorkoutAsync(newWorkout);

            return Results.Created($"/workout/{workout.Id}", workout);

        });


    }

}