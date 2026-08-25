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

}