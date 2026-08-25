using Gym.API.Dtos;
using Gym.API.Models;
using Gym.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Gym.API.Endpoints;

public static class ExerciseEndpoints
{
    public static void MapExerciseEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/exercises");
        group.MapGet("/", async (ExerciseService exerciseService) =>
        {
            //var exercises = await dbContext.Exercises.ToListAsync();
            var exercises = await exerciseService.GetAllExercisesAsync();
            return Results.Ok(exercises);

        });
        group.MapGet("/{id}", async (int id, GymContext dbContext) =>

               {
                   var ex = await dbContext.Exercises.FindAsync(id);

                   return ex is null ? Results.NotFound() : Results.Ok(
                       new ExerciseDetailDto(
                           ex.Id,
                           ex.Name,
                           ex.Reps
                           ));

               });

        group.MapPost("/", async (CreateExerciseDto newExercise, ExerciseService exerciseService) =>
            {
                // Exercise exercise = new()
                // {
                //     Name = newExercise.Name,
                //     Reps = newExercise.Reps
                // };
                // exerciseService.Exercises.Add(exercise);
                // await exerciseService.SaveChangesAsync();
                // return Results.Created($"/exercise/{exercise.Id}", exercise);
                var exercise = await exerciseService.CreateExerciseAsync(newExercise);
                return Results.Created($"/exercise/{exercise.Id}", exercise)
            });

        group.MapPut("/{id}", async (int id, UpdateExerciseDto updatedExercise, GymContext dbContext) =>
        {
            var existingExercise = await dbContext.Exercises.FindAsync(id);
            if (existingExercise is null)
            {
                return Results.NotFound();
            }
            existingExercise.Name = updatedExercise.Name;
            existingExercise.Reps = updatedExercise.Reps;
            await dbContext.SaveChangesAsync();
            return Results.NoContent();

        });

        group.MapDelete("/{id}", async (int id, GymContext dbContext) =>
        {
            await dbContext.Exercises.Where(Exercise => Exercise.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });


    }
}