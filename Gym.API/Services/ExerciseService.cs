using Gym.API.Models;
using Gym.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Gym.API.Services;

public class ExerciseService(GymContext dbContext)
{
    public async Task<List<Exercise>> GetAllExercisesAsync()
    {
        return await dbContext.Exercises.ToListAsync();
    }

    public async Task<Exercise> CreateExerciseAsync(CreateExerciseDto newExercise)
    {
        Exercise exercise = new()
        {
            Name = newExercise.Name,
            Reps = newExercise.Reps
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
            return null;
        }
        existingExercise.Name = updatedExercise.Name;
        existingExercise.Reps = updatedExercise.Reps;
        await dbContext.SaveChangesAsync();

        return existingExercise;
    }
}