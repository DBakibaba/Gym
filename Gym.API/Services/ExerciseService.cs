using Gym.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.API.Services;

public class ExerciseService(GymContext dbContext)
{
    public async Task<List<Exercise>> GetAllExercisesAsync()
    {
        return await dbContext.Exercises.ToListAsync();
    }

}