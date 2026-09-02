using Gym.API.Models;
using Gym.API.Dtos;

using Microsoft.EntityFrameworkCore;

namespace Gym.API.Services;

public class ExerciseService(GymContext dbContext, ILogger<ExerciseService> logger)
{
    public async Task<List<Exercise>> GetAllExercisesAsync()
    {
        return await dbContext.Exercises.Include(exercise => exercise.Workout).ToListAsync();
    }
    public async Task<Exercise?> GetExercisesByIdAsync(int id)
    {
        var exercise = await dbContext.Exercises.FindAsync(id);

        return exercise;
    }
    public async Task<List<Exercise>> GetExercisesByWorkoutIdAsync(int id)
    {
        return await dbContext.Exercises.Where(exercise => exercise.WorkoutId == id).ToListAsync();
    }
    public async Task<Exercise?> CreateExerciseAsync(CreateExerciseDto newExercise)
    {
        logger.LogInformation("Creating exercise {ExerciseName} for workout {WorkoutId}",
                                newExercise.Name,
                                newExercise.WorkoutId);

        var workoutExist = await dbContext.Workouts.AnyAsync(workout => workout.Id == newExercise.WorkoutId);
        if (!workoutExist)
        {
            logger.LogWarning("Workout {WorkoutId} was not found when creating exercise", newExercise.WorkoutId);
            return null;
        }
        Exercise exercise = new()
        {
            Name = newExercise.Name.ToUpper(),
            Reps = newExercise.Reps,
            WorkoutId = newExercise.WorkoutId
        };
        dbContext.Exercises.Add(exercise);
        await dbContext.SaveChangesAsync();

        return exercise;
    }

    public async Task<Exercise?> UpdateExerciseAsync(int id, UpdateExerciseDto updatedExercise)
    {
        var existingExercise = await dbContext.Exercises.FindAsync(id);
        if (existingExercise is null)
        {
            logger.LogWarning("Exercise {ExerciseId} was not found for update", id);
            return null;
        }
        existingExercise.Name = updatedExercise.Name;
        existingExercise.Reps = updatedExercise.Reps;
        await dbContext.SaveChangesAsync();

        return existingExercise;
    }
    public async Task<bool> DeleteExerciseAsync(int id)
    {
        var exercise = await dbContext.Exercises.FindAsync(id);

        if (exercise is null)
        {
            logger.LogWarning("Exercise {exerciseId} was not found for deletion.", id);
            return false;
        }

        dbContext.Exercises.Remove(exercise);
        logger.LogInformation("Deleted exercise {ExerciseId}", id);
        await dbContext.SaveChangesAsync();

        return true;
    }







}