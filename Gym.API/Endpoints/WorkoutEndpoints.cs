using Gym.API.Dtos;
using Gym.API.Models;
using Gym.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Gym.API.Endpoints;

public static class WorkoutEndpoints
{
    public static void MapWorkoutEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/workouts");
        group.MapGet("/", async (WorkoutService workoutService) =>
        {
            var workouts = await workoutService.GetAllWorkoutsAsync();

            return Results.Ok(workouts);
        });

        group.MapGet("/{id}", async (int id, WorkoutService workoutService) =>
        {
            var workout = await workoutService.GetWorkoutByIdAsync(id);
            return workout is null ? Results.NotFound() : Results.Ok(
                new WorkoutDetailDto(
                    workout.Id,
                    workout.Name
                ));
        });


        group.MapPost("/", async (CreateWorkoutDto newWorkout, WorkoutService workoutService) =>
        {
            var workout = await workoutService.CreateWorkoutAsync(newWorkout);

            return Results.Created($"/workout/{workout.Id}", workout);

        });

        group.MapPut("/{id}", async (int id, UpdateWorkoutDto updateWorkout, WorkoutService workoutService) =>
        {
            var workout = await workoutService.UpdateWorkoutAsync(id, updateWorkout);
            return workout is null ? Results.NotFound() : Results.NoContent();

        });

        group.MapDelete("/{id}", async (int id, WorkoutService workoutService) =>
        {
            var deleted = await workoutService.DeleteWorkoutAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }

}