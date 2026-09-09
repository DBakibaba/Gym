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
      var exercises = await exerciseService.GetAllExercisesAsync();

      var exerciseDtos = exercises.Select(exercise =>
          new ExerciseDetailDto(
              exercise.Id,
              exercise.Name,
              exercise.Reps,
              exercise.WorkoutId,
              exercise.Workout.Name
          ));

      return Results.Ok(exerciseDtos);
  });
        group.MapGet("/{id}", async (int id, ExerciseService exerciseService) =>

               {
                   var exercise = await exerciseService.GetExercisesByIdAsync(id);

                   return exercise is null ? Results.NotFound() : Results.Ok(
                       new ExerciseDetailDto(
                           exercise.Id,
                           exercise.Name,
                           exercise.Reps,
                           exercise.WorkoutId,
                           exercise.Workout.Name
                           ));

               }).WithName("GetExerciseById").WithSummary("Get an exercise by ID").WithDescription("Return one exercise including its workout information.").Produces<ExerciseDetailDto>(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound);

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
                if (exercise is null)
                {
                    return Results.NotFound(
                        new
                        {
                            message = $"Workout{newExercise.WorkoutId} was not found."
                        });
                }

                return Results.CreatedAtRoute("GetExerciseById",
                new { id = exercise.Id }, exercise);
            });

        group.MapPut("/{id}", async (int id, UpdateExerciseDto updatedExercise, ExerciseService exerciseService) =>
        {
            var exercise = await exerciseService.UpdateExerciseAsync(id, updatedExercise);

            return exercise is null
                ? Results.NotFound()
                : Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, ExerciseService exerciseService) =>
        {
            var deleted = await exerciseService.DeleteExerciseAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });


    }
}